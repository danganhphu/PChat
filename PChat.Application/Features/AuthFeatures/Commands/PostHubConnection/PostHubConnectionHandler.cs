using MediatR;
using PChat.Application.Services;

namespace PChat.Application.Features.AuthFeatures.Commands.PostHubConnection;

public class PostHubConnectionHandler(IAuthService authService) : IRequestHandler<PostHubConnectionCommand, string>
{

    public async Task<string> Handle(PostHubConnectionCommand request, CancellationToken cancellationToken)
    {
        return await authService.PostHubConnection(request, cancellationToken);
    }
}