using CSharpClicker.Dtos;
using MediatR;

namespace CSharpClicker.UseCases.GetCurrentUserInfo;

public record GetCurrentUserInfoQuery : IRequest<UserInfoDto>;
