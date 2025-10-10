using CSharpClicker.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CSharpClicker.UseCases.Register;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Unit>
{
    private readonly UserManager<ApplicationUser> userManager;

    public RegisterUserCommandHandler(UserManager<ApplicationUser> userManager)
    {
        this.userManager = userManager;
    }

    public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser
        {
            UserName = request.UserName,
        };

        await userManager.CreateAsync(user, request.Password);

        return Unit.Value;
    }
}
