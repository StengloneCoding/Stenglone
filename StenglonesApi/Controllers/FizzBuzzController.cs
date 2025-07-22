namespace StenglonesApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using StenglonesApi.Services;

    [ApiController]
    [Route("api/[controller]")]
    public class FizzBuzzController : ControllerBase
    {
        private readonly FizzBuzzService _fizzBuzzService;

        public FizzBuzzController(FizzBuzzService fizzBuzzService)
        {
            _fizzBuzzService = fizzBuzzService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] int start, [FromQuery] int end)
        {
            if (start > end)
                return BadRequest("Start must be less than or equal to end.");

            if (end - start > 1000)
                return BadRequest("Maximum range of 1000 allowed.");

            var result = _fizzBuzzService.Generate(start, end);
            return Ok(result);
        }
    }

}