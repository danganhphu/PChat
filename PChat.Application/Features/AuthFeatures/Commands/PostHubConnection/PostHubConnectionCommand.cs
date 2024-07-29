using MediatR;
using PChat.Application.Bases;

namespace PChat.Application.Features.AuthFeatures.Commands.PostHubConnection;

public sealed record PostHubConnectionCommand(
    string Key) : IRequest<BaseResponse<string>>;