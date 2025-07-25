namespace StenglonesApi.Services;

using StenglonesApi.DTOs;
using StenglonesApi.Models;
using StenglonesApi.Data;
using StenglonesApi.Interfaces;
using StenglonesApi.Utils;
using StenglonesApi.Exceptions;
using System.Security.Principal;

public class MachineMetricsService : IMachineMetricsService
{
    private readonly AppDbContext _context;
    private readonly ILogger<MachineMetricsService> _logger;

    public MachineMetricsService(AppDbContext context, ILogger<MachineMetricsService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task SaveMetricAsync(MachineMetricDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = dto.ToEntity();
            _context.MachineMetrics.Add(entity);
            _logger.LogInformation("Metric saved successfully.");
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException dbEx)
        {
            _logger.LogError(dbEx, "Database update failed while saving metric.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while saving the metric.");
            throw;
        }
    }

    public async Task UpdateMetricAsync(int id, MachineMetricDto dto, CancellationToken cancellationToken = default)
    {

        var entity = await _context.MachineMetrics.Where(m => m.Id == id).FirstOrDefaultAsync(cancellationToken);
        if (entity == null)
        {
            string errorMessage = $"Metric with ID {id} not found.";
            _logger.LogWarning(errorMessage);
            throw new NotFoundException($"Not Found: {errorMessage}");
        }

        dto.MapToExisting(entity);

        try
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException dbEx)
        {
            _logger.LogError(dbEx, $"Database update failed while updating metric with the id: {id}.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An unexpected error occurred while updating the metric id: {id}.");
            throw;
        }
    }

    public async Task<List<MachineMetric>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.MachineMetrics.ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving machine metrics.");
            throw;
        }
    }
}

