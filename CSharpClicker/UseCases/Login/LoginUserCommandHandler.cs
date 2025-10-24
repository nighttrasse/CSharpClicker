using CSharpClicker.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CSharpClicker.UseCases.Login;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Unit>
{
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly UserManager<ApplicationUser> userManager;

    public LoginUserCommandHandler(SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager)
    {
        this.signInManager = signInManager;
        this.userManager = userManager;
    }

    public async Task<Unit> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByNameAsync(request.UserName);

        if (user == null)
        {
            throw new ValidationException("Пользователь с указаным ником не был найден.");
        }

        var result = await signInManager.PasswordSignInAsync(user, request.Password, isPersistent: true, lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            throw new ValidationException("Неверный пароль");
        }

        return Unit.Value;
    }
}
