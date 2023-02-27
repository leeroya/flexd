using FluentValidation;
using FlexD.Contracts.Interfaces.Options;
using FlexD.Core.ErrorHandling;

namespace FlexD.Core.Validators;

public class ConnectionPropertiesValidator: AbstractValidator<IConnectionProperties>
{
    public ConnectionPropertiesValidator()
    {
      RuleFor(x => x)
        .Must(IsUsingConnectionString)
        .WithMessage(ErrorMessages.ConnectionStringMustNotBeEmpty);

      When(IsUsingConnectionString, () =>
      {
        RuleFor(x => x.ConnectionString).Empty().WithMessage(ErrorMessages.ConnectionStringMustNotBeEmpty);
      });
    }
    
    private bool IsUsingConnectionString(IConnectionProperties properties)
    {
      return !string.IsNullOrWhiteSpace(properties.ConnectionString);
    }
}