using FluentValidation;

namespace FizzBuzz.Application.Features.Commands;

public class ProcessFizzBuzzCommandValidator : AbstractValidator<ProcessFizzBuzzCommand>
{
    public ProcessFizzBuzzCommandValidator()
    {
        RuleFor(x => x.Input).NotEmpty()
            .WithMessage("Input must be provided");
    }
}