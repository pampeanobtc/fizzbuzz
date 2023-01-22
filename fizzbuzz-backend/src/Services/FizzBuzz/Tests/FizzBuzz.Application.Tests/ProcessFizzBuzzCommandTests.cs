using FizzBuzz.Application.Contracts.Persistence;
using FizzBuzz.Application.Features.Commands;
using FizzBuzz.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;

namespace FizzBuzz.Application.Tests;

public class ProcessFizzBuzzCommandTests
{
    [Fact]
    public async void ProcessFizzBuzzCommand_HandledCorrectly()
    {
        //Arrange
        var repoMock = new Mock<IFileRepository>();
        var loggerMock = new Mock<ILogger<ProcessFizzBuzzCommandHandler>>();
        var input = 10;
        var max = 20;
        var processor = new FizzBuzzProcessor(input, DateTimeOffset.UtcNow);
        var limit = processor.GetRandomLimit(max);
        processor.ProcessFizzBuzz(limit);

        var handler = new ProcessFizzBuzzCommandHandler(repoMock.Object, loggerMock.Object);

        //Act
        var command = new ProcessFizzBuzzCommand()
        {
            Input = input,
            Max = max
        };
        string result = await handler.Handle(command, new CancellationToken());

        //Assert
        var maxRange = limit - input + 1;
        var totalFizz = processor.ToString().Split(',')[1..maxRange];
        var resultFizz = result.Split(',')[1..];
        Assert.Contains(totalFizz.ToString(), resultFizz.ToString()!);
        repoMock.Verify(x=>x.SaveProcessedResults(It.IsAny<string>()), Times.Once);
    }
}