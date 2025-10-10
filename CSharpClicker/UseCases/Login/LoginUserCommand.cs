using MediatR;

namespace CSharpClicker.UseCases.Login;

public record LoginUserCommand(string UserName, string Password) : IRequest<Unit>;
