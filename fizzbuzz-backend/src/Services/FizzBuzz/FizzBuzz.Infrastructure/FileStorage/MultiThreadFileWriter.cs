using System.Collections.Concurrent;
using FizzBuzz.Application.Contracts.FileStorage;
using FizzBuzz.Application.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FizzBuzz.Infrastructure.FileStorage;


public class MultiThreadFileWriter : IMultiThreadFileWriter
{
    private static ConcurrentQueue<string> _textToWrite = new();
    private CancellationTokenSource _source = new();
    private CancellationToken _token;
    private readonly FileStorageSettings _fileStorageSettings;

    public MultiThreadFileWriter(IConfiguration configuration, ILogger<MultiThreadFileWriter> logger)
    {
        _token = _source.Token;
        _fileStorageSettings = configuration.GetSection(FileStorageSettings.FileStorage).Get<FileStorageSettings>() ??
                               throw new ArgumentNullException(nameof(configuration));
    }
    
    public void WriteLine(string line)
    {
        _textToWrite.Enqueue(line);
        Task.Run(WriteToFileThreadSafe, _token);
    }
    
    private static ReaderWriterLockSlim _readWriteLock = new ReaderWriterLockSlim();
    
    public void WriteToFileThreadSafe() {
        // Set Status to Locked
        _readWriteLock.EnterWriteLock();
        try
        {
            if (_token.IsCancellationRequested)
            {
                return;
            }
            // Append text to the file
            using (StreamWriter sw = File.AppendText(_fileStorageSettings.FilePath))
            {
                while (_textToWrite.TryDequeue(out var textLine))
                {
                    sw.WriteLine(textLine);
                }
                sw.Flush();
                sw.Close();
            }
        }
        finally
        {
            // Release lock
            _readWriteLock.ExitWriteLock();
        }
    }
    // public async void WriteToFile()
    // {
    //     while (true)
    //     {
    //         if (_token.IsCancellationRequested)
    //         {
    //             return;
    //         }
    //         await using var w = File.AppendText(_fileStorageSettings.FilePath!);
    //         while (_textToWrite.TryDequeue(out var textLine))
    //         {
    //             await w.WriteLineAsync(textLine);
    //         }
    //         await w.FlushAsync();
    //         Thread.Sleep(100);
    //     }
    // }
}