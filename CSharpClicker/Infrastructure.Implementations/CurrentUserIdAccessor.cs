using CSharpClicker.Infrastructure.Abstractions;
using System.Security.Claims;

namespace CSharpClicker.Infrastructure.Implementations;

public class CurrentUserIdAccessor : ICurrentUserIdAccessor
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public CurrentUserIdAccessor(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    public Guid? GetCurrentUserId()
    {
        return Guid.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
    }
}
