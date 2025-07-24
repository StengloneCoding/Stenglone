using System.Collections.Generic;
using TechTalk.SpecFlow;
using Xunit;
using StenglonesApi.Services;

namespace StenglonesApi.Tests.Features.StepDefinitions;

[Binding]
public class FizzBuzzSteps
{
    private readonly ScenarioContext _context;
    private readonly FizzBuzzService _service = new();
    private List<string> _result = new();

    public FizzBuzzSteps(ScenarioContext context)
    {
        _context = context;
    }

    [When(@"I generate FizzBuzz from (.*) to (.*)")]
    public void WhenIGenerateFizzBuzzFromTo(int start, int end)
    {
        _result = _service.Generate(start, end);
    }

    [Then(@"the result should contain exactly one item")]
    public void ThenTheResultShouldContainExactlyOneItem()
    {
        Assert.Single(_result);
    }

    [Then(@"the first value should be ""(.*)""")]
    public void ThenTheFirstValueShouldBe(string expected)
    {
        Assert.Equal(expected, _result[0]);
    }

    [Then(@"the result should contain (.*) items")]
    public void ThenTheResultShouldContainItems(int count)
    {
        Assert.Equal(count, _result.Count);
    }
}
