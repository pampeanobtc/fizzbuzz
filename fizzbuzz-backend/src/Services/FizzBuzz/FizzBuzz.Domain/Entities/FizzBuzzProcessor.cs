using FizzBuzz.Domain.Common;
using FizzBuzz.Domain.Common.Constants;

namespace FizzBuzz.Domain.Entities;

public class FizzBuzzProcessor : EntityBase
{
    private readonly List<string> _processedItems;
    private readonly int _input;

    public FizzBuzzProcessor(int input, DateTimeOffset signingTime)
    {
        _input = input;
        _processedItems = new List<string>();
        
        SigningTimestamp = signingTime.UtcTicks;
    }

    public void ProcessFizzBuzz(int limit)
    {
        EnsureLimitIsHigherThanInput(_input, limit);

        for (var item = _input; item <= limit; item++)
        {
            if (IsDivisibleByThree(item) && IsDivisibleByFive(item))
            {
                _processedItems.Add(FizzBuzzConstants.FIZZBUZZ);
            } else if (IsDivisibleByThree(item))
            {
                _processedItems.Add(FizzBuzzConstants.FIZZ);
            } else if (IsDivisibleByFive(item))
            {
                _processedItems.Add(FizzBuzzConstants.BUZZ);
            } else
            {
                _processedItems.Add(item.ToString());
            }
        }
    }

    public int GetRandomLimit(int max)
    {
        EnsureMaxIsWithinBoundaries(max);
        EnsureMaxIsHigherThanInput(_input, max);
        
        var rnd = new Random();
        return rnd.Next(_input + 1, max + 1);
    }

    private static void EnsureMaxIsWithinBoundaries(int max)
    {
        if (max is > 1000000 or < 0) // this can be made configurable
        {
            throw new ArgumentException("Max randomizer must be within 0 and 1000000", nameof(max));
        }    
    }

    private static void EnsureMaxIsHigherThanInput(int input, int max)
    {
        if (input >= max)
        {
            throw new ArgumentException("Max randomizer must be higher or equal to input", nameof(max));
        }
    }

    public List<string> GetProcessedItems()
    {
        return _processedItems;
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
        return string.Concat($"timestamp:{SigningTimestamp.ToString()},", string.Join(',', _processedItems));
    }
}