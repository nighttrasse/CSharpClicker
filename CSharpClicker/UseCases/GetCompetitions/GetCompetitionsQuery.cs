using CSharpClicker.Dtos;
using MediatR;

namespace CSharpClicker.UseCases.GetCompetitions;

public record GetCompetitionsQuery : IRequest<IEnumerable<CompetitionDto>>;
