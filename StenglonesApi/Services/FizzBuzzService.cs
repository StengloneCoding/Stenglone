namespace StenglonesApi.Services
{
    public class FizzBuzzService
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
}
