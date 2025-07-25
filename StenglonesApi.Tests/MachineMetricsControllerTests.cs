using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using StenglonesApi.Controllers;
using StenglonesApi.DTOs;
using StenglonesApi.Interfaces;


public class MachineMetricsControllerTests
{
    private readonly Mock<IMachineMetricsService> _mockService = new();
    private readonly Mock<ILogger<MachineMetricsController>> _mockLogger = new();
    private readonly MachineMetricsController _controller;

    public MachineMetricsControllerTests()
    {
        _controller = new MachineMetricsController(_mockService.Object, _mockLogger.Object);
    }

    [Theory]
    [InlineData(-150)] 
    [InlineData(1500)] 
    public async Task Post_TemperatureOutOfRange_ReturnsBadRequest(double temp)
    { 
        var dto = new MachineMetricDto { Temperature = temp, RotationSpeed = 500 };

 
        var result = await _controller.Post(dto, CancellationToken.None);


        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var problemDetails = Assert.IsType<ProblemDetails>(badRequestResult.Value);

        Assert.Equal(400, problemDetails.Status);
        Assert.Equal("Temperature is out of range", problemDetails.Title);
        Assert.Contains("Temperature is out of range", problemDetails.Detail);
    }

    [Theory]
    [InlineData(-150)]
    [InlineData(1500)]
    public async Task Put_TemperatureOutOfRange_ReturnsBadRequest(double temp)
    {
        var dto = new MachineMetricDto { Temperature = temp, RotationSpeed = 500 };

        var result = await _controller.Put(1, dto, CancellationToken.None);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var problemDetails = Assert.IsType<ProblemDetails>(badRequestResult.Value);

        Assert.Equal(400, problemDetails.Status);
        Assert.Equal("Temperature is out of range", problemDetails.Title);
        Assert.Contains("Temperature is out of range", problemDetails.Detail);
    }


}
