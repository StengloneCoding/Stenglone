namespace StenglonesApi.Tests;

using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StenglonesApi.Services;
using StenglonesApi.Data;
using StenglonesApi.DTOs;
using StenglonesApi.Models;

public class MachineMetricsServiceTests
{
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
        // Arrange
        var context = GetInMemoryDbContext();
        var service = new MachineMetricsService(context);
        var dto = new MachineMetricDto
        {
            Temperature = 25.5,
            RotationSpeed = 1500,
            Timestamp = DateTime.UtcNow
        };

        // Act
        await service.SaveMetricAsync(dto);

        // Assert
        var savedMetrics = await context.MachineMetrics.ToListAsync(TestContext.Current.CancellationToken);
        Assert.Single(savedMetrics);
        Assert.Equal(dto.Temperature, savedMetrics[0].Temperature);
        Assert.Equal(dto.RotationSpeed, savedMetrics[0].RotationSpeed);
        Assert.Equal(dto.Timestamp, savedMetrics[0].Timestamp);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllSavedMetrics()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var service = new MachineMetricsService(context);

        context.MachineMetrics.Add(new MachineMetric { Temperature = 20, RotationSpeed = 1000, Timestamp = DateTime.UtcNow });
        context.MachineMetrics.Add(new MachineMetric { Temperature = 30, RotationSpeed = 2000, Timestamp = DateTime.UtcNow });
        await context.SaveChangesAsync(TestContext.Current.CancellationToken);

        // Act
        var result = await service.GetAllAsync(TestContext.Current.CancellationToken);

        // Assert
        Assert.Equal(2, result.Count);
    }
}
