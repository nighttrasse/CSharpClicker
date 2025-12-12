using CSharpClicker.Infrastructure.Abstractions;
using CSharpClicker.UseCases.GetCompetitionInvitations;
using CSharpClicker.UseCases.GetCompetitions;
using CSharpClicker.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSharpClicker.Controllers;

[Route("competitions")]
[Authorize]
public class CompetitionController : Controller
{
    private readonly IMediator mediator;
    private readonly ICurrentUserIdAccessor currentUserIdAccessor;

    public CompetitionController(IMediator mediator, ICurrentUserIdAccessor currentUserIdAccessor)
    {
        this.mediator = mediator;
        this.currentUserIdAccessor = currentUserIdAccessor;
    }

    [HttpGet("home")]
    public async Task<IActionResult> Competitions()
    {
        var competitions = await mediator.Send(new GetCompetitionsQuery());
        var competitionInivitations = await mediator.Send(new GetCompetitionInvitationsQuery());
        var currentUserId = currentUserIdAccessor.GetCurrentUserId()
            ?? throw new InvalidOperationException("Current user ID is not available.");

        var model = new CompetitionsViewModel
        { 
            CurrentUserId = currentUserId,
            Competitions = competitions,
            CompetitionInvitations = competitionInivitations
        };

        return View(model);
    }
}
