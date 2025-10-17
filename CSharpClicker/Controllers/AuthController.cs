using CSharpClicker.UseCases.Login;
using CSharpClicker.UseCases.Logout;
using CSharpClicker.UseCases.Register;
using CSharpClicker.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CSharpClicker.Controllers;

[Route("auth")]
public class AuthController : Controller
{
    private IMediator mediator;

    public AuthController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserCommand command)
    {
        await mediator.Send(command);

        return RedirectToAction("Login");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserCommand command)
    {
        await mediator.Send(command);

        return RedirectToAction("Index", controllerName: "Home");
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout(LogoutUserCommand command)
    {
        await mediator.Send(command);

        return RedirectToAction("Login");
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        var viewModel = new LoginViewModel();

        return View(viewModel);
    }

    [HttpGet("register")]
    public IActionResult Register()
    {
        var viewModel = new RegisterViewModel();

        return View(viewModel);
    }
}
