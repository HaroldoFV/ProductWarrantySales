using System.Runtime.CompilerServices;
using ProductMicroservice.Domain.Exceptions;

namespace ProductMicroservice.Domain.Validation;

public class DomainValidation
{
    public static void NotNullOrEmpty(string value, [CallerArgumentExpression("value")] string? fieldName = null)
    {
        if (string.IsNullOrEmpty(value))
            throw new EntityValidationException($"{fieldName} cannot be null or empty.");
    }

    public static void MinLength(string value, int min, [CallerArgumentExpression("value")] string? fieldName = null)
    {
        if (value.Length < min)
            throw new EntityValidationException($"{fieldName} should be at least {min} characters long.");
    }

    public static void MaxLength(string value, int max, [CallerArgumentExpression("value")] string? fieldName = null)
    {
        if (value.Length > max)
            throw new EntityValidationException($"{fieldName} should be no more than {max} characters long.");
    }

    public static void GreaterThan<T>(T value, T comparisonValue,
        [CallerArgumentExpression("value")] string? fieldName = null)
        where T : IComparable<T>
    {
        if (value.CompareTo(comparisonValue) <= 0)
            throw new EntityValidationException($"{fieldName} should be greater than {comparisonValue}.");
    }

    public static void GreaterOrEqualThan<T>(T value, T comparisonValue,
        [CallerArgumentExpression("value")] string? fieldName = null)
        where T : IComparable<T>
    {
        if (value.CompareTo(comparisonValue) < 0)
            throw new EntityValidationException($"{fieldName} should be greater than or equal to {comparisonValue}.");
    }

    public static void LessOrEqualThan<T>(T value, T comparisonValue,
        [CallerArgumentExpression("value")] string? fieldName = null)
        where T : IComparable<T>
    {
        if (value.CompareTo(comparisonValue) > 0)
            throw new EntityValidationException($"{fieldName} should be less than or equal to {comparisonValue}.");
    }
}