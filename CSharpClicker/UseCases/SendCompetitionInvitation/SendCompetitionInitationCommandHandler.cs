using AutoMapper;
using CSharpClicker.Domain;
using CSharpClicker.Dtos;
using CSharpClicker.Infrastructure.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CSharpClicker.UseCases.SendCompetitionInvitation;

public class SendCompetitionInitationCommandHandler : IRequestHandler<SendCompetitionInvitationCommand, CompetitionInvitationDto>
{
    private readonly IMapper mapper;
    private readonly IAppDbContext appDbContext;
    private readonly ICurrentUserIdAccessor currentUserIdAccessor;

    public SendCompetitionInitationCommandHandler(
        IMapper mapper,
        IAppDbContext appDbContext,
        ICurrentUserIdAccessor currentUserIdAccessor)
    {
        this.mapper = mapper;
        this.appDbContext = appDbContext;
        this.currentUserIdAccessor = currentUserIdAccessor;
    }

    public async Task<CompetitionInvitationDto> Handle(SendCompetitionInvitationCommand request, CancellationToken cancellationToken)
    {
        var userId = currentUserIdAccessor.GetCurrentUserId()
            ?? throw new InvalidOperationException("Unauthenticated user cannot send invites.");

        var user = await appDbContext.Users.FirstAsync(u => u.Id == userId, cancellationToken);
        var toUser = await appDbContext.Users.FirstAsync(u => u.Id == request.ToUserId, cancellationToken);

        var competitionInvitation = new CompetitionInvitation
        {
            FromUserId = user.Id,
            ToUserId = toUser.Id,
            SentAt = DateTime.UtcNow,
        };

        appDbContext.CompetitionInvitations.Add(competitionInvitation);

        await appDbContext.SaveChangesAsync(cancellationToken);

        return mapper.Map<CompetitionInvitationDto>(competitionInvitation);
    }
}
