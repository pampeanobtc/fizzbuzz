using FizzBuzz.Domain.Common.Constants;
using FizzBuzz.Domain.Entities;

namespace FizzBuzz.Domain.Tests;

public class FizzBuzzProcessorTests
{
    [Theory]
    [InlineData(1, 100)]
    [InlineData(25, 230)]
    [InlineData(20013, 230490)]
    [InlineData(200, 10293)]
    [InlineData(10000, 20000)]
    [InlineData(0, 1)]
    [InlineData(0, 2)]
    public void GivenValidInputParams_ShouldReturnCorrectFizzAndBuzz(int input, int limit)
    {
        // arrange
        var timestamp = DateTimeOffset.UtcNow;
        var processor = new FizzBuzzProcessor(input, limit, timestamp);
        // act
        processor.ProcessFizzBuzz();
        // assert
        Assert.NotEmpty(processor.Items);
        Assert.True(input < limit);
        
        var expectedCount = limit - input + 1;
        var numberList = Enumerable.Range(0, expectedCount);
        Assert.Equal(expectedCount, processor.Items.Count);
        
        AssertResults(input, numberList, processor.Items);
        Assert.Equal(timestamp.UtcTicks, processor.SigningTimestamp);
    }

    private static void AssertResults(int input, IEnumerable<int> numberList, List<string> result)
    {
        Parallel.ForEach(numberList, index =>
        {
            var item = result.ElementAt(index);
            var originalItemValue = index + input;
            if (originalItemValue % 5 == 0 && originalItemValue % 3 == 0)
            {
                Assert.Equal(FizzBuzzConstants.FIZZBUZZ, item);
            }
            else if (originalItemValue % 5 == 0)
            {
                Assert.Equal(FizzBuzzConstants.BUZZ, item);
            }
            else if (originalItemValue % 3 == 0)
            {
                Assert.Equal(FizzBuzzConstants.FIZZ, item);
            }
            else
            {
                Assert.Equal(originalItemValue, int.Parse(item));
            }
        });
    }

    [Theory]
    [InlineData(10, 4)]
    [InlineData(345, 2)]
    [InlineData(21, 21)]
    public void GivenGreaterInputThanLimit_ShouldReturn_OutOfBoundaryError(int input, int limit)
    {
        // arrange
        var processor = new FizzBuzzProcessor(input, limit, DateTimeOffset.UtcNow);
        // act
        Assert.Throws<ArgumentException>(() => processor.ProcessFizzBuzz());
    }

}