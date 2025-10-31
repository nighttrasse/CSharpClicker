using AutoMapper;
using CSharpClicker.DomainServices;
using CSharpClicker.Dtos;
using CSharpClicker.Infrastructure.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CSharpClicker.UseCases.GetCurrentUserInfo;

public class GetCurrentUserInfoQueryHandler : IRequestHandler<GetCurrentUserInfoQuery, UserInfoDto>
{
    private readonly IAppDbContext appDbContext;
    private readonly ICurrentUserIdAccessor currentUserIdAccessor;
    private IMapper mapper;

    public GetCurrentUserInfoQueryHandler(IAppDbContext appDbContext, ICurrentUserIdAccessor currentUserIdAccessor,
        IMapper mapper)
    {
        this.appDbContext = appDbContext;
        this.currentUserIdAccessor = currentUserIdAccessor;
        this.mapper = mapper;
    }

    public async Task<UserInfoDto> Handle(GetCurrentUserInfoQuery request, CancellationToken cancellationToken)
    {
        var userId = currentUserIdAccessor.GetCurrentUserId();

        if (userId == null)
        {
            throw new InvalidOperationException("User was not identified");
        }

        var user = await appDbContext.Users
            .Include(u => u.UserBoosts)
            .FirstAsync(u => u.Id == userId);

        var userDto = mapper.Map<UserInfoDto>(user);

        userDto.ProfitPerClick = user.UserBoosts.GetProfitPerClick();
        userDto.ProfitPerSecond = user.UserBoosts.GetProfitPerSecond();

        return userDto;
    }
}
