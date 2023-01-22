using FizzBuzz.Domain.Common;
using FizzBuzz.Domain.Common.Constants;

namespace FizzBuzz.Domain.Entities;

public class FizzBuzzProcessor : EntityBase
{
    private readonly List<string> _processedItems;

    public FizzBuzzProcessor(int input, int limit, DateTimeOffset signingTime)
    {
        _input = input;
        _limit = limit;
        _processedItems = new List<string>();
        
        SigningTimestamp = signingTime.UtcTicks;
    }

    private readonly int _input;
    private readonly int _limit;

    public void ProcessFizzBuzz()
    {
        EnsureLimitIsHigherOrEqualToInput(_input, _limit);

        for (var item = _input; item <= _limit; item++)
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

    public List<string> GetProcessedItems()
    {
        return _processedItems;
    }

    private static void EnsureLimitIsHigherOrEqualToInput(int input, int limit)
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