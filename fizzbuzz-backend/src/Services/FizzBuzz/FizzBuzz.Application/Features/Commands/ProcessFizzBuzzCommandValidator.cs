using FluentValidation;

namespace FizzBuzz.Application.Features.Commands;

public class ProcessFizzBuzzCommandValidator : AbstractValidator<ProcessFizzBuzzCommand>
{
    public ProcessFizzBuzzCommandValidator()
    {
        RuleFor(x => x.Input).NotEmpty().Must((model, field) => model.Limit > field);
    }
}