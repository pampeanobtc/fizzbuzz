using FizzBuzz.Application.Models;
using FizzBuzz.Infrastructure.FileStorage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;

namespace FizzBuzz.Infrastructure.Tests;

public class ConcurrencyTests
{
    [Fact]
    public async Task ConcurrentStorage()
    {
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        var settings = config.GetSection(FileStorageSettings.FileStorage).Get<FileStorageSettings>();
        File.Delete(settings!.FilePath);
        var fileWriter = new MultiThreadFileWriter(config, new Mock<ILogger<MultiThreadFileWriter>>().Object);

        // act
        for (var index = 0; index < 10; index++)
        {
            var taskArray = new Task[100];
            for (var i = 0; i < taskArray.Length; i++) {
                taskArray[i] = Task.Factory.StartNew( x => {
                    var threadId = Thread.CurrentThread.ManagedThreadId;
                    fileWriter.WriteLine($"line {x} to thread {threadId}");
                }, i);
            }
            Task.WaitAll(taskArray);
        }

        // assert
        Assert.Equal(1000, File.ReadAllLines(settings!.FilePath).Length);
    }
}