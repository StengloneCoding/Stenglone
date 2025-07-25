namespace StenglonesApi.Controllers;

using StenglonesApi.DTOs;
using StenglonesApi.Exceptions;
using StenglonesApi.Interfaces;
using StenglonesApi.Models;
using StenglonesApi.Services;
using StenglonesApi.Utils;

[ApiController]
[Route("[controller]")]
public class MachineMetricsController(IMachineMetricsService machineMetricsService, ILogger<MachineMetricsController> logger) : ControllerBase
{
    private readonly IMachineMetricsService _machineMetricsService = machineMetricsService;
    private readonly ILogger<MachineMetricsController> _logger = logger;

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] MachineMetricDto dto, CancellationToken cancellationToken)
    {
        if (dto is { Temperature: < -100 or > 1000 })
        {
            var tempCelsius = TemperatureConverter.FahrenheitToCelsius(dto.Temperature).ToString("F1");
            _logger.LogError("Temperature is out of range: {Temp}°C", tempCelsius);
            return BadRequest($"Temperature is out of range: {tempCelsius}°C");
        }

        try
        {
            await _machineMetricsService.SaveMetricAsync(dto, cancellationToken);
            return Ok("Metric saved.");
        }
        catch (DbUpdateException dbEx)
        {
            _logger.LogError(dbEx, "Database operation failed.");
            return StatusCode(500, "An error occurred while saving the metric.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred.");
            return StatusCode(500, "An unexpected error occurred.");
        }
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] MachineMetricDto dto, CancellationToken cancellationToken)
    {
        if (dto is { Temperature: < -100 or > 1000 })
        {
            var tempCelsius = TemperatureConverter.FahrenheitToCelsius(dto.Temperature).ToString("F1");
            _logger.LogError("Temperature is out of range: {Temp}°C", tempCelsius);
            return BadRequest($"Temperature is out of range: {tempCelsius}°C");
        }

        try
        {
            await _machineMetricsService.UpdateMetricAsync(id, dto, cancellationToken);
            return Ok("Metric saved.");
        }
        catch (NotFoundException ex)
        {
            _logger.LogWarning(ex, "Metric not found.");
            return NotFound(new { message = ex.Message });
        }
        catch (DbUpdateException dbEx)
        {
            _logger.LogError(dbEx, "Database operation failed.");
            return StatusCode(500, "An error occurred while updating the metric.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred.");
            return StatusCode(500, "An unexpected error occurred.");
        }
    }


    [HttpGet]
    public async Task<ActionResult<List<MachineMetric>>> Get(CancellationToken cancellationToken)
    {
        try
        {
            var data = await _machineMetricsService.GetAllAsync(cancellationToken);
            return Ok(data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving machine metrics.");
            return StatusCode(500, "An unexpected error occurred.");
        }
    }
}