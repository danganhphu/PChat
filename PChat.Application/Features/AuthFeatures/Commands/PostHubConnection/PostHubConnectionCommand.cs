using MediatR;

namespace PChat.Application.Features.AuthFeatures.Commands.PostHubConnection;

public sealed record PostHubConnectionCommand(
    string Key) : IRequest<string>;