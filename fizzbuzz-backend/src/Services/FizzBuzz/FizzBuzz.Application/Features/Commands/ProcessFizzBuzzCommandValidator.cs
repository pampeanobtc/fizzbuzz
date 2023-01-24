using FluentValidation;

namespace FizzBuzz.Application.Features.Commands;

public class ProcessFizzBuzzCommandValidator : AbstractValidator<ProcessFizzBuzzCommand>
{
    public ProcessFizzBuzzCommandValidator()
    {
        RuleFor(x => x.Input).GreaterThanOrEqualTo(0)
            .WithMessage("Input must be provided");
    }
}