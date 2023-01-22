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
    public void GivenValidInputParams_ShouldReturnCorrectFizzAndBuzz(int input, int max)
    {
        // arrange
        var timestamp = DateTimeOffset.UtcNow;
        var processor = new FizzBuzzProcessor(input, timestamp);
        var limit = processor.GetRandomLimit(max);
        // act
        processor.ProcessFizzBuzz(limit);
        // assert
        var processedItems = processor.GetProcessedItems();
        Assert.NotEmpty(processedItems);
        Assert.True(input < limit);
        
        var expectedCount = limit - input + 1;
        var numberList = Enumerable.Range(0, expectedCount);
        Assert.Equal(expectedCount, processedItems.Count);
        
        AssertResults(input, numberList, processedItems);
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
    [InlineData(10, 5)]
    [InlineData(10, 10)]
    [InlineData(10, 2)]
    public void GivenGreaterInputThanMaxRandomizer_ShouldReturn_OutOfBoundaryError(int input, int max)
    {
        // arrange
        var processor = new FizzBuzzProcessor(input, DateTimeOffset.UtcNow);
        // act // assert
        Assert.Throws<ArgumentException>(() => processor.GetRandomLimit(max));
    }
    
    [Theory]
    [InlineData(10)]
    [InlineData(345)]
    [InlineData(21)]
    public void GivenGreaterInputThanLimit_ShouldReturn_OutOfBoundaryError(int input)
    {
        // arrange
        var processor = new FizzBuzzProcessor(input, DateTimeOffset.UtcNow);
        // act
        Assert.Throws<ArgumentException>(() => processor.ProcessFizzBuzz(input));
        Assert.Throws<ArgumentException>(() => processor.ProcessFizzBuzz(input - 1));
    }

}