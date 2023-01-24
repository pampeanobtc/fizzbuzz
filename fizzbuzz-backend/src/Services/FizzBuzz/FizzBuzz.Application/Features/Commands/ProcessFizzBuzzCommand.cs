using FizzBuzz.Application.Models;
using MediatR;

namespace FizzBuzz.Application.Features.Commands;

public struct ProcessFizzBuzzCommand : IRequest<FizzBuzzResultModel>
{
    public int Input { get; set; }
    public int Limit { get; set; }
}
