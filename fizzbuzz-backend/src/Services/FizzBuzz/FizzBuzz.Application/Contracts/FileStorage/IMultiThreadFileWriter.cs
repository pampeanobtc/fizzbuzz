namespace FizzBuzz.Application.Contracts.FileStorage;

public interface IMultiThreadFileWriter
{
    void WriteLine(string line);
}