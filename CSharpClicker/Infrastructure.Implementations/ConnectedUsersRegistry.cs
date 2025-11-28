using CSharpClicker.Infrastructure.Abstractions;
using System.Collections.Concurrent;

namespace CSharpClicker.Infrastructure.Implementations;

public class ConnectedUsersRegistry : IConnectedUsersRegistry
{
    private readonly ConcurrentDictionary<string, Guid> connectedUsers = new();

    public void AddUser(string connectionId, Guid userId)
    {
        connectedUsers.TryAdd(connectionId, userId);
    }

    public IReadOnlyCollection<Guid> GetAllConnectedUsers()
        => connectedUsers.Values
           .Distinct()
           .ToArray();

    public void RemoveUser(string connectionId)
    {
        connectedUsers.Remove(connectionId, out _);
    }
}
