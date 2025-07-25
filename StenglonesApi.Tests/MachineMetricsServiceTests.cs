namespace StenglonesApi.Tests;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StenglonesApi.Data;
using StenglonesApi.DTOs;
using StenglonesApi.Models;
using StenglonesApi.Services;
using Xunit;

public class MachineMetricsServiceTests
{
    private readonly AppDbContext _context;
    private readonly ILogger<MachineMetricsService> _logger;
    private readonly MachineMetricsService _service;

    public MachineMetricsServiceTests()
    {
        _context = GetInMemoryDbContext();
        _logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<MachineMetricsService>();
        _service = new MachineMetricsService(_context, _logger);
    }

    private AppDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    [Fact]
    public async Task SaveMetricAsync_AddsMetricToDatabase()
    {
        var dto = new MachineMetricDto
        {
            Temperature = 25.5,
            RotationSpeed = 1500,
            CreatedAtTimestamp = DateTime.UtcNow
        };

        await _service.SaveMetricAsync(dto, CancellationToken.None);

        var savedMetrics = await _context.MachineMetrics.ToListAsync(CancellationToken.None);
        Assert.Single(savedMetrics);
        Assert.Equal(dto.Temperature, savedMetrics[0].Temperature);
        Assert.Equal(dto.RotationSpeed, savedMetrics[0].RotationSpeed);
        Assert.Equal(dto.CreatedAtTimestamp, savedMetrics[0].CreatedAtTimestamp);
    }

    [Fact]
    public async Task UpdateMetricAsync_UpdatesMetricInDatabase()
    {
        var dto = new MachineMetricDto
        {
            Temperature = 25.5,
            RotationSpeed = 1500
        };

        {
            _context.MachineMetrics.Add(new MachineMetric { Temperature = 20, RotationSpeed = 1000, CreatedAtTimestamp = DateTime.UtcNow });
            await _context.SaveChangesAsync(CancellationToken.None);

            await _service.UpdateMetricAsync(1, dto, CancellationToken.None);

            var savedMetrics = await _context.MachineMetrics.ToListAsync(CancellationToken.None);
            Assert.Single(savedMetrics);
            Assert.Equal(dto.Temperature, savedMetrics[0].Temperature);
            Assert.Equal(dto.RotationSpeed, savedMetrics[0].RotationSpeed);
            Assert.NotNull(savedMetrics[0].UpdatedAtTimestamp);
            Assert.True(savedMetrics[0].UpdatedAtTimestamp > DateTime.UtcNow.AddMinutes(-1));

        }
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllSavedMetrics()
    {
        _context.MachineMetrics.Add(new MachineMetric { Temperature = 20, RotationSpeed = 1000, CreatedAtTimestamp = DateTime.UtcNow });
        _context.MachineMetrics.Add(new MachineMetric { Temperature = 30, RotationSpeed = 2000, CreatedAtTimestamp = DateTime.UtcNow });

        await _context.SaveChangesAsync(CancellationToken.None);

        var result = await _service.GetAllAsync(CancellationToken.None);

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task UpdateMetricAsync_ThrowsKeyNotFoundException_WhenEntityNotFound()
    {
        var dto = new MachineMetricDto { Temperature = 10, RotationSpeed = 1000 };
        var service = new MachineMetricsService(_context, _logger);

        var ex = await Assert.ThrowsAsync<KeyNotFoundException>(() => service.UpdateMetricAsync(999, dto));
        Assert.Contains("not found", ex.Message);
    }

}
