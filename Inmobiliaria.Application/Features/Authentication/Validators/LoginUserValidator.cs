using FluentValidation;
using Inmobiliaria.Application.Features.Authentication.Commands.LoginAuth;

namespace Inmobiliaria.Application.Features.Authentication.Validators;

public class LoginUserValidator : AbstractValidator<LoginAuthCommand>
{
    public LoginUserValidator()
    {
        RuleFor(x => x.UserCredential)
              .NotEmpty().WithMessage("El campo {PropertyName} es requerido")
              .MaximumLength(100).WithMessage("{PropertyName} no puede superar los 100 caracteres")
              .Must(IsValidEmailOrUser)
              .WithMessage("{PropertyName} debe ser un correo válido o un nombre de usuario válido");

        RuleFor(x => x.Password)
              .NotEmpty().WithMessage("El campo {PropertyName} es requerido")
              .MinimumLength(8).WithMessage("{PropertyName} debe tener al menos 8 caracteres")
              .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$")
              .WithMessage("{PropertyName} debe contener al menos una mayúscula, una minúscula, un número y un carácter especial");
    }

    private bool IsValidEmailOrUser(string credential)
    {
        if (string.IsNullOrWhiteSpace(credential))
            return false;

        if (credential.Contains('@'))
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(credential);
                return addr.Address == credential;
            }
            catch
            {
                return false;
            }
        }

        return credential.Length >= 3 && credential.Length <= 50;
    }
}
