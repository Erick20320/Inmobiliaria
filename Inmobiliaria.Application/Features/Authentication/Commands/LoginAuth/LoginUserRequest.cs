namespace Inmobiliaria.Application.Features.Authentication.Commands.LoginAuth;

public sealed record LoginUserRequest(
    string UserCredential,
    string Password
)
{
    public LoginAuthCommand ToCommand()
        => new(UserCredential, Password);
}
