using MediatR;

namespace CSharpClicker.UseCases.Register;

public record RegisterUserCommand(string UserName, string Password) : IRequest<Unit>;
