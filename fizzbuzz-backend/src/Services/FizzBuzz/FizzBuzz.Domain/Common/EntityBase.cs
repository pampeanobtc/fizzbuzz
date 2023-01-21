namespace FizzBuzz.Domain.Common;

public abstract class EntityBase
{
    public Guid Id { get; protected set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset Modified { get; set; }
}