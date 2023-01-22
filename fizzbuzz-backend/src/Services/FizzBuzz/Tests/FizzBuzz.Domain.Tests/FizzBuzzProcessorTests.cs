using FizzBuzz.Domain.Common.Constants;
using FizzBuzz.Domain.Entities;

namespace FizzBuzz.Domain.Tests;

public class FizzBuzzProcessorTests
{
    [Theory]
    [InlineData(1, 100)]
    [InlineData(3, 25)]
    [InlineData(2, 20013)]
    [InlineData(144, 200)]
    [InlineData(0, 10000)]
    public void GivenValidInputParams_ShouldReturnCorrectFizzAndBuzz(int input, int limit)
    {
        // arrange
        var timestamp = DateTimeOffset.UtcNow;
        var processor = new FizzBuzzProcessor(input, limit, timestamp);
        var expectedCount = limit - input + 1;
        var numberList = Enumerable.Range(0, expectedCount);
        // act
        processor.ProcessFizzBuzz();
        // assert
        var processedItems = processor.GetProcessedItems();
        Assert.NotEmpty(processedItems);
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
    public void GivenGreaterInputThanLimit_ShouldReturn_OutOfBoundaryError(int input, int limit)
    {
        // arrange
        var processor = new FizzBuzzProcessor(input, limit, DateTimeOffset.UtcNow);
        // act
        Assert.Throws<ArgumentException>(() => processor.ProcessFizzBuzz());
    }

}