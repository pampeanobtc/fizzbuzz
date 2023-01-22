using FluentValidation;

namespace FizzBuzz.Application.Features.Commands;

public class ProcessFizzBuzzCommandValidator : AbstractValidator<ProcessFizzBuzzCommand>
{
    public ProcessFizzBuzzCommandValidator()
    {
        RuleFor(x => x.Input)
            .Must((model, input) => model.Limit > input)
            .WithMessage("Limit must be higher than Input");
    }
}