namespace StenglonesApi.Tests;

using StenglonesApi.Services;

public class FizzBuzzServiceTests
{
    private readonly FizzBuzzService _service = new();

    [Theory]
    [InlineData(1, "1")]
    [InlineData(3, "Fizz")]
    [InlineData(5, "Buzz")]
    [InlineData(15, "FizzBuzz")]
    public void Generate_SingleValue_ReturnsExpected(int input, string expected)
    {
        var result = _service.Generate(input, input);
        Assert.Single(result);
        Assert.Equal(expected, result[0]);
    }

    [Fact]
    public void Generate_Range_ReturnsCorrectCount()
    {
        int start = 1, end = 100;
        var result = _service.Generate(start, end);
        Assert.Equal(100, result.Count);
    }
}
