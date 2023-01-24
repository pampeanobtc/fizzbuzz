namespace FizzBuzz.Application.Models;

public class FizzBuzzResultModel
{
    public List<string> Items { get; set; }
    public long Signature { get; set; }

    public override string ToString()
    {
        return string.Concat($"timestamp:{Signature.ToString()},", string.Join(',', Items));;
    }
}