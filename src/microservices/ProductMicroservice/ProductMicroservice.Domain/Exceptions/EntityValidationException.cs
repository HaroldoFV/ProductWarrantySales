﻿using ProductMicroservice.Domain.Validation;

namespace ProductMicroservice.Domain.Exceptions;

public class EntityValidationException : Exception
{
    public IReadOnlyCollection<ValidationError>? Errors { get; }

    public EntityValidationException(
        string? message,
        IReadOnlyCollection<ValidationError>? errors = null
    ) : base(message)
        => Errors = errors;
}