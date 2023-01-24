namespace FizzBuzz.Application.Models;

public class ErrorResponse
{
    public string Type { get; }
    public string Message { get; }
    public string StackTrace { get; }

    public ErrorResponse(Exception ex)
    {
        Type = ex.GetType().Name;
        Message = ex.Message;
        StackTrace = ex.ToString();
    }
}