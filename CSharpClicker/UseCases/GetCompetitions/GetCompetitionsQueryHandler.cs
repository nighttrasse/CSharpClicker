using AutoMapper;
using CSharpClicker.Dtos;
using CSharpClicker.Infrastructure.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CSharpClicker.UseCases.GetCompetitions;

public class GetCompetitionsQueryHandler : IRequestHandler<GetCompetitionsQuery, IEnumerable<CompetitionDto>>
{
    private readonly ICurrentUserIdAccessor currentUserIdAccessor;
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;

    public GetCompetitionsQueryHandler(
        ICurrentUserIdAccessor currentUserIdAccessor,
        IAppDbContext appDbContext,
        IMapper mapper)
    {
        this.currentUserIdAccessor = currentUserIdAccessor;
        this.appDbContext = appDbContext;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<CompetitionDto>> Handle(GetCompetitionsQuery request, CancellationToken cancellationToken)
    {
        var userId = currentUserIdAccessor.GetCurrentUserId()
            ?? throw new InvalidOperationException("Current user ID is not available.");

        var competitionInvitations = appDbContext.Competitions
            .Where(ci => ci.FirstUserId == userId || ci.SecondUserId == userId)
            .Include(ci => ci.FirstUser)
            .Include(ci => ci.SecondUser);

        return await mapper.ProjectTo<CompetitionDto>(competitionInvitations)
            .ToArrayAsync(cancellationToken);
    }
}
