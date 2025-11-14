using MediatR;

namespace CSharpClicker.UseCases.RegisterClicks
{
    public record RegisterClicksCommand(int ClickCount) : IRequest<Unit>;
}
