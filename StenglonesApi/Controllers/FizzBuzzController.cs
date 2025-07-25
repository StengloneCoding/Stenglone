namespace StenglonesApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StenglonesApi.Interface;
using StenglonesApi.Interfaces;

[ApiController]
[Route("[controller]")]
public class FizzBuzzController(IFizzBuzzService fizzBuzzService, ILogger<FizzBuzzController> logger) : ControllerBase
{
    private readonly IFizzBuzzService _fizzBuzzService = fizzBuzzService;
    private readonly ILogger<FizzBuzzController> _logger = logger;

    [HttpGet]
    public IActionResult Get([FromQuery] int start, [FromQuery] int end)
    {
        if (start > end)
        {
            const string msg = "Start must be less than or equal to end.";
            _logger.LogWarning(msg + " start={Start}, end={End}", start, end);
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Invalid range",
                Detail = msg,
                Instance = HttpContext.Request.Path
            });
        }

        if (end - start > 1000)
        {
            const string msg = "Maximum range of 1000 allowed.";
            _logger.LogWarning(msg + " range={Range}", end - start);
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Range too large",
                Detail = msg,
                Instance = HttpContext.Request.Path
            });
        }

        var result = _fizzBuzzService.Generate(start, end);
        return Ok(result);
    }
}
