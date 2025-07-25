namespace StenglonesApi.Controllers;

using StenglonesApi.DTOs;
using StenglonesApi.Interfaces;
using StenglonesApi.Models;
using StenglonesApi.Services;
using StenglonesApi.Utils;


/// <summary>
/// Provides endpoints to manage machine metrics.
/// </summary>
[ApiController]
[Route("[controller]")]
public class MachineMetricsController(IMachineMetricsService machineMetricsService, ILogger<MachineMetricsController> logger) : ControllerBase
{
    private readonly IMachineMetricsService _machineMetricsService = machineMetricsService;
    private readonly ILogger<MachineMetricsController> _logger = logger;


    /// <summary>
    /// Adds a new machine metric.
    /// </summary>
    /// <param name="dto">The metric data to add.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Confirmation message or validation/problem details.</returns>
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post([FromBody] MachineMetricDto dto, CancellationToken cancellationToken)
    {
        if (dto is { Temperature: < -100 or > 1000 })
        {
            var tempCelsius = TemperatureConverter.FahrenheitToCelsius(dto.Temperature).ToString("F1");
            _logger.LogError("Temperature is out of range: {Temp}°C", tempCelsius);
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Temperature is out of range",
                Detail = $"Temperature is out of range: {tempCelsius}°C"
            });
        }
        await _machineMetricsService.SaveMetricAsync(dto, cancellationToken);
        return Ok("Metric saved.");
    }

    /// <summary>
    /// Updates an existing machine metric.
    /// </summary>
    /// <param name="id">The ID of the metric to update.</param>
    /// <param name="dto">The updated metric data.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Confirmation message or validation/problem details.</returns>
    [HttpPut("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Put(int id, [FromBody] MachineMetricDto dto, CancellationToken cancellationToken)
    {
        if (dto is { Temperature: < -100 or > 1000 })
        {
            var tempCelsius = TemperatureConverter.FahrenheitToCelsius(dto.Temperature).ToString("F1");
            _logger.LogError("Temperature is out of range: {Temp}°C", tempCelsius);
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Temperature is out of range",
                Detail = $"Temperature is out of range: {tempCelsius}°C"
            });
        }
        await _machineMetricsService.UpdateMetricAsync(id, dto, cancellationToken);
        return Ok("Metric saved.");
    }

    /// <summary>
    /// Gets all machine metrics.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>List of all machine metrics.</returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<MachineMetric>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<MachineMetric>>> Get(CancellationToken cancellationToken)
    {
        var data = await _machineMetricsService.GetAllAsync(cancellationToken);
        return Ok(data);

    }
}