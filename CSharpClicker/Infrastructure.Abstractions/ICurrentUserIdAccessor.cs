namespace CSharpClicker.Infrastructure.Abstractions;

public interface ICurrentUserIdAccessor
{
    Guid? GetCurrentUserId();
}
