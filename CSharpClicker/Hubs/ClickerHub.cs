using CSharpClicker.Infrastructure.Abstractions;
using CSharpClicker.UseCases.BuyBoost;
using CSharpClicker.UseCases.RegisterClicks;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace CSharpClicker.Hubs;

public class ClickerHub : Hub
{
    private readonly IMediator mediator;
    private readonly IConnectedUsersRegistry connectedUsersRegistry;

    public ClickerHub(IMediator mediator, IConnectedUsersRegistry connectedUsersRegistry)
    {
        this.mediator = mediator;
        this.connectedUsersRegistry = connectedUsersRegistry;
    }

    public async Task RegisterClicks(int clickCount)
        => await mediator.Send(new RegisterClicksCommand(clickCount));

    public async Task BuyBoost(int boostId)
        => await mediator.Send(new BuyBoostCommand(boostId));

    public async Task ScoreUpdated(Guid userId, long current, long record, CancellationToken cancellationToken)
        => await Clients.User(userId.ToString()).SendAsync("ScoreUpdated", current, record, cancellationToken);

    public async Task BoostUpdated(Guid userId, int boostId, int quantity, long currentPrice, CancellationToken cancellationToken)
        => await Clients.User(userId.ToString()).SendAsync("BoostUpdated", boostId, quantity, currentPrice, cancellationToken);

    public async Task ProfitUpdated(Guid userId, long profitPerClick, long profitPerSecond, CancellationToken cancellationToken)
        => await Clients.User(userId.ToString()).SendAsync("ProfitUpdated", profitPerClick, profitPerSecond, cancellationToken);

    public override Task OnConnectedAsync()
    {
        var userIdString = Context.UserIdentifier;
        var connectionId = Context.ConnectionId;

        if (Guid.TryParse(userIdString, out var userId))
        {
            connectedUsersRegistry.AddUser(connectionId, userId);
        }

        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        var connectionId = Context.ConnectionId;

        connectedUsersRegistry.RemoveUser(connectionId);

        return base.OnDisconnectedAsync(exception);
    }
}
