using CSharpClicker.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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
        if ((await userManager.Users.AnyAsync(u => u.UserName == request.UserName, cancellationToken)))
        {
            throw new ValidationException("Пользователь с таким ником уже существует");
        }

        var user = new ApplicationUser
        {
            UserName = request.UserName,
        };

        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            throw new ValidationException(string.Join(';', result.Errors.Select(e => e.Description)));
        }

        return Unit.Value;
    }
}
