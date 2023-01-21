using FluentValidation;
using MediatR;
using ValidationException = FizzBuzz.Application.Exceptions.ValidationException;

namespace FizzBuzz.Application.Behaviors;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults =
                await Task.WhenAll(_validators.Select(x => x.ValidateAsync(context, cancellationToken)));
            var errors = validationResults.SelectMany(x => x.Errors).Where(error => error != null).ToList();
            if (errors.Any())
            {
                throw new ValidationException(errors);
            }
        }

        return await next();
    }
}