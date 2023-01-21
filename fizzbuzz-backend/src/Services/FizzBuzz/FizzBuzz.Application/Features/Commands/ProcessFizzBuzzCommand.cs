using MediatR;

namespace FizzBuzz.Application.Features.Commands;

public struct ProcessFizzBuzzCommand : IRequest<string>
{
    public int Input { get; set; }
    public int Limit { get; set; }
}
