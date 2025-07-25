namespace StenglonesApi.Services;

using StenglonesApi.Interface;

public class FizzBuzzService : IFizzBuzzService
{
    public List<string> Generate(int start, int end)
    {
        var list = new List<string>();
        for (int i = start; i <= end; i++)
        {
            list.Add((i % 15 == 0) ? "FizzBuzz" :
                      (i % 3 == 0) ? "Fizz" :
                      (i % 5 == 0) ? "Buzz" :
                      i.ToString());
        }
        return list;
    }
}

