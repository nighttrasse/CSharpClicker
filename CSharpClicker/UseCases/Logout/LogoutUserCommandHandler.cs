using CSharpClicker.Domain;
using CSharpClicker.UseCases.Login;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CSharpClicker.UseCases.Logout;

public class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand, Unit>
{
    private readonly SignInManager<ApplicationUser> signInManager;

    public LogoutUserCommandHandler(SignInManager<ApplicationUser> signInManager)
    {
        this.signInManager = signInManager;
    }


    public async Task<Unit> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        await signInManager.SignOutAsync();

        return Unit.Value;
    }
}
