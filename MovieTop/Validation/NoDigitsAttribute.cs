using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MovieTop.Validation;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class NoDigitsAttribute : ValidationAttribute, IClientModelValidator
{
    public NoDigitsAttribute()
    {
        ErrorMessage = "Название фильма не должно содержать цифры.";
    }

    protected override ValidationResult? IsValid(
        object? value,
        ValidationContext validationContext)
    {
        if (value is string text && text.Any(char.IsDigit))
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;
    }

    public void AddValidation(ClientModelValidationContext context)
    {
        MergeAttribute(
            context.Attributes,
            "data-val",
            "true");

        MergeAttribute(
            context.Attributes,
            "data-val-nodigits",
            ErrorMessage ?? "Название фильма не должно содержать цифры.");
    }

    private static bool MergeAttribute(
        IDictionary<string, string> attributes,
        string key,
        string value)
    {
        if (attributes.ContainsKey(key))
        {
            return false;
        }

        attributes.Add(key, value);
        return true;
    }
}