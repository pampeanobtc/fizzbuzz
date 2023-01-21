namespace FizzBuzz.Application.Contracts.Persistence;

public interface IFileRepository
{
    public void SaveProcessedResults(string line);
}