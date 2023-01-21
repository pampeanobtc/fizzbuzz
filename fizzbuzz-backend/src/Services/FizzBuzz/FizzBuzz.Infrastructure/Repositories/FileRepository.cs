using FizzBuzz.Application.Contracts.FileStorage;
using FizzBuzz.Application.Contracts.Persistence;

namespace FizzBuzz.Infrastructure.Repositories;

public class FileRepository : IFileRepository
{
    private readonly IMultiThreadFileWriter _fileWriter;

    public FileRepository(IMultiThreadFileWriter fileWriter)
    {
        _fileWriter = fileWriter;
    }
    public void SaveProcessedResults(string line)
    {
        _fileWriter.WriteLine(line);
    }
}