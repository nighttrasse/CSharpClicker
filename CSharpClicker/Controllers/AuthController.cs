using CSharpClicker.UseCases.Login;
using CSharpClicker.UseCases.Logout;
using CSharpClicker.UseCases.Register;
using CSharpClicker.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
        try
        {
            await mediator.Send(command);
        }
        catch (ValidationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            var viewModel = new RegisterViewModel()
            {
                UserName = command.UserName,
            };

            return View(viewModel);
        }

        return RedirectToAction("Login");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserCommand command)
    {
        try
        {
            await mediator.Send(command);
        }
        catch (ValidationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            var viewModel = new LoginViewModel()
            {
                UserName = command.UserName,
            };

            return View(viewModel);
        }

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
