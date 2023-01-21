using FizzBuzz.Application.Contracts.Persistence;
using FizzBuzz.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FizzBuzz.Application.Features.Commands;

public class ProcessFizzBuzzCommandHandler : IRequestHandler<ProcessFizzBuzzCommand, string>
{
    private readonly IFileRepository _fileRepository;
    private readonly ILogger<ProcessFizzBuzzCommandHandler> _logger;

    public ProcessFizzBuzzCommandHandler(IFileRepository fileRepository, ILogger<ProcessFizzBuzzCommandHandler> logger)
    {
        _fileRepository = fileRepository;
        _logger = logger;
    }
    
    public Task<string> Handle(ProcessFizzBuzzCommand request, CancellationToken cancellationToken)
    {
        var processor = new FizzBuzzProcessor(request.Input, request.Limit, DateTimeOffset.UtcNow);
        _logger.LogInformation("adding line to fizzbuzz. timestamp: {timestamp}", processor.SigningTimestamp);
        processor.ProcessFizzBuzz();
        _fileRepository.SaveProcessedResults(processor.ToString());
        _logger.LogInformation("added line to fizzbuzz. timestamp: {timestamp}", processor.SigningTimestamp);

        return Task.FromResult(processor.ToString());
    }
}