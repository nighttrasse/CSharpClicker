using AutoMapper;
using CSharpClicker.Dtos;
using CSharpClicker.Infrastructure.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CSharpClicker.UseCases.GetCompetitionInvitations;

public class GetCompetitionInvitationsQueryHandler : IRequestHandler<GetCompetitionInvitationsQuery, IEnumerable<CompetitionInvitationDto>>
{
    private readonly IMapper mapper;
    private readonly ICurrentUserIdAccessor currentUserIdAccessor;
    private readonly IAppDbContext appDbContext;

    public GetCompetitionInvitationsQueryHandler(
        IMapper mapper,
        ICurrentUserIdAccessor currentUserIdAccessor,
        IAppDbContext appDbContext)
    {
        this.mapper = mapper;
        this.currentUserIdAccessor = currentUserIdAccessor;
        this.appDbContext = appDbContext;
    }

    public async Task<IEnumerable<CompetitionInvitationDto>> Handle(GetCompetitionInvitationsQuery request, CancellationToken cancellationToken)
    {
        var userId = currentUserIdAccessor.GetCurrentUserId()
            ?? throw new InvalidOperationException("Current user ID is not available.");

        var competitionInvitations = appDbContext.CompetitionInvitations
            .Where(ci => ci.ToUserId == userId || ci.FromUserId == userId)
            .Include(ci => ci.FromUser)
            .Include(ci => ci.ToUser);

        return await mapper.ProjectTo<CompetitionInvitationDto>(competitionInvitations)
            .ToArrayAsync(cancellationToken);
    }
}
