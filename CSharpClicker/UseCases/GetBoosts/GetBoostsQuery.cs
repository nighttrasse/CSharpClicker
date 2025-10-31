using CSharpClicker.Dtos;
using MediatR;

namespace CSharpClicker.UseCases.GetBoosts;

public record GetBoostsQuery : IRequest<IEnumerable<BoostDto>>;
