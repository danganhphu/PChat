using FluentValidation;
using FluentValidation.Results;
using MediatR;
using PChat.Application.Abstractions.Messaging;
using PChat.Application.Exceptions;

namespace PChat.Application.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommandBase
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // Validate
        var context = new ValidationContext<TRequest>(request);

        var validationFailures = await Task.WhenAll(
            validators.Select(validator => validator.ValidateAsync(context, cancellationToken)));

        var errors = validationFailures
            .Where(validationResult => !validationResult.IsValid)
            .SelectMany(validationResult => validationResult.Errors)
            .Select(validationFailure => new ValidationError(
                validationFailure.PropertyName,
                validationFailure.ErrorMessage))
            .ToList();

        if (errors.Any())
        {
            throw new Exceptions.ValidationException(errors);
        }

        var response = await next();

        return response;
    }
}