
namespace CSharpClicker.Infrastructure.Abstractions;

public interface IConnectedUsersRegistry
{
    IReadOnlyCollection<Guid> GetAllConnectedUsers();

    void AddUser(string connectionId, Guid userId);

    void RemoveUser(string connectionId);
}
