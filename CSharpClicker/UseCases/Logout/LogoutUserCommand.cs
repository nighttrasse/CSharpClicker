using MediatR;

namespace CSharpClicker.UseCases.Logout;

public record LogoutUserCommand : IRequest<Unit>;
