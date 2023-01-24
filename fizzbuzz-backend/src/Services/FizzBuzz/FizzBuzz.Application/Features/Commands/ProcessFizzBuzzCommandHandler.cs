using AutoMapper;
using FizzBuzz.Application.Contracts.Persistence;
using FizzBuzz.Application.Models;
using FizzBuzz.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FizzBuzz.Application.Features.Commands;

public class ProcessFizzBuzzCommandHandler : IRequestHandler<ProcessFizzBuzzCommand, FizzBuzzResultModel>
{
    private readonly IFileRepository _fileRepository;
    private readonly ILogger<ProcessFizzBuzzCommandHandler> _logger;
    private readonly IMapper _mapper;

    public ProcessFizzBuzzCommandHandler(IFileRepository fileRepository, ILogger<ProcessFizzBuzzCommandHandler> logger, IMapper mapper)
    {
        _fileRepository = fileRepository;
        _logger = logger;
        _mapper = mapper;
    }
    
    public Task<FizzBuzzResultModel> Handle(ProcessFizzBuzzCommand request, CancellationToken cancellationToken)
    {
        var processor = new FizzBuzzProcessor(request.Input, request.Limit, DateTimeOffset.UtcNow);
        processor.ProcessFizzBuzz();
        _logger.LogInformation("adding line to fizzbuzz. timestamp: {timestamp}", processor.SigningTimestamp);
        _fileRepository.SaveProcessedResults(processor.ToString());
        _logger.LogInformation("added line to fizzbuzz. timestamp: {timestamp}", processor.SigningTimestamp);
        var fizzBuzzResult = _mapper.Map<FizzBuzzResultModel>(processor);
        return Task.FromResult(fizzBuzzResult);
    }
}