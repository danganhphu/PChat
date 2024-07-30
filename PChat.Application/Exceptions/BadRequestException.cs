using FluentValidation.Results;

namespace PChat.Application.Exceptions;

public sealed class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message)
    {

    }

    public BadRequestException(string message, ValidationResult validationResult) : base(message)
    {
        ValidationErrors = validationResult.ToDictionary();
    }

    public IDictionary<string, string[]> ValidationErrors { get; set; }
}