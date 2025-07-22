namespace StenglonesApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using StenglonesApi.DTOs;
    using StenglonesApi.Models;
    using StenglonesApi.Services;
    using System.Threading;

    [ApiController]
    [Route("api/[controller]")]
    public class MachineMetricsController : ControllerBase
    {
        private readonly MachineMetricsService _service;

        public MachineMetricsController(MachineMetricsService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MachineMetricDto dto)
        {
            if (dto.Temperature < -100 || dto.Temperature > 1000)
                return BadRequest("Temperature value out of range.");

            await _service.SaveMetricAsync(dto);
            return Ok("Metric saved.");
        }

        [HttpGet]
        public async Task<ActionResult<List<MachineMetric>>> Get(CancellationToken cancellationToken)
        {
            var data = await _service.GetAllAsync(cancellationToken);
            return Ok(data);
        }
    }



}