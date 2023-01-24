using FizzBuzz.Domain.Common;
using FizzBuzz.Domain.Common.Constants;

namespace FizzBuzz.Domain.Entities;

public class FizzBuzzProcessor : EntityBase
{
    public List<string> Items { get; }
    private readonly int _input;
    private readonly int _limit;

    public FizzBuzzProcessor(int input, int limit, DateTimeOffset signingTime)
    {
        _input = input;
        Items = new List<string>();

        _limit = limit;
        SigningTimestamp = signingTime.UtcTicks;
    }

    public void ProcessFizzBuzz()
    {
        EnsureMaxIsWithinBoundaries(_limit);
        EnsureLimitIsHigherThanInput(_input, _limit);

        for (var item = _input; item <= _limit; item++)
        {
            if (IsDivisibleByThree(item) && IsDivisibleByFive(item))
            {
                Items.Add(FizzBuzzConstants.FIZZBUZZ);
            } else if (IsDivisibleByThree(item))
            {
                Items.Add(FizzBuzzConstants.FIZZ);
            } else if (IsDivisibleByFive(item))
            {
                Items.Add(FizzBuzzConstants.BUZZ);
            } else
            {
                Items.Add(item.ToString());
            }
        }
    }

    private static void EnsureMaxIsWithinBoundaries(int max)
    {
        if (max is > 1000000 or < 0) // this can be made configurable
        {
            throw new ArgumentException("Max randomizer must be within 0 and 1000000", nameof(max));
        }    
    }
    
    private static void EnsureLimitIsHigherThanInput(int input, int limit)
    {
        if (input >= limit)
        {
            throw new ArgumentException("Limit must be higher or equal to input", nameof(limit));
        }
    }

    private static bool IsDivisibleByFive(int item)
    {
        return item % 5 == 0;
    }

    private static bool IsDivisibleByThree(int item)
    {
        return item % 3 == 0;
    }

    public override string ToString()
    {
        return string.Concat($"timestamp:{SigningTimestamp.ToString()},", string.Join(',', Items));
    }
}