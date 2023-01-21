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
        var limit = 20;
        var processor = new FizzBuzzProcessor(input, limit, DateTimeOffset.UtcNow);
        var command = new ProcessFizzBuzzCommand()
        {
            Input = input,
            Limit = limit
        };
        processor.ProcessFizzBuzz();
        var handler = new ProcessFizzBuzzCommandHandler(repoMock.Object, loggerMock.Object);

        //Act
        string result = await handler.Handle(command, new CancellationToken());

        //Assert
        Assert.Equal(processor.ToString().Split(',')[1..], result.Split(',')[1..]);
        repoMock.Verify(x=>x.SaveProcessedResults(It.IsAny<string>()), Times.Once);
    }
}