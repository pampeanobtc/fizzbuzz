using AutoMapper;
using FizzBuzz.Application.Contracts.Persistence;
using FizzBuzz.Application.Features.Commands;
using FizzBuzz.Application.Mappings;
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
        var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        var mapper = config.CreateMapper();
        
        var input = 10;
        var limit = 20;
        var processor = new FizzBuzzProcessor(input, limit, DateTimeOffset.UtcNow);
        processor.ProcessFizzBuzz();

        var handler = new ProcessFizzBuzzCommandHandler(repoMock.Object, loggerMock.Object, mapper);

        //Act
        var command = new ProcessFizzBuzzCommand()
        {
            Input = input,
            Limit = limit
        };
        var result = await handler.Handle(command, new CancellationToken());

        //Assert
        Assert.Contains(string.Join(',', result.Items), processor.ToString());
        repoMock.Verify(x=>x.SaveProcessedResults(It.IsAny<string>()), Times.Once);
    }
}