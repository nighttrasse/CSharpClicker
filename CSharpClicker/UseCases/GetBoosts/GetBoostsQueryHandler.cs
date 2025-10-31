using AutoMapper;
using CSharpClicker.Dtos;
using CSharpClicker.Infrastructure.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CSharpClicker.UseCases.GetBoosts;

public class GetBoostsQueryHandler : IRequestHandler<GetBoostsQuery, IEnumerable<BoostDto>>
{
	private readonly IAppDbContext appDbContext;
	private readonly IMapper mapper;

	public GetBoostsQueryHandler(IAppDbContext appDbContext, IMapper mapper)
	{
		this.appDbContext = appDbContext;
		this.mapper = mapper;
	}

	public async Task<IEnumerable<BoostDto>> Handle(GetBoostsQuery request, CancellationToken cancellationToken)
	{
		return await mapper.ProjectTo<BoostDto>(appDbContext.Boosts, cancellationToken)
			.ToArrayAsync();
	}
}
