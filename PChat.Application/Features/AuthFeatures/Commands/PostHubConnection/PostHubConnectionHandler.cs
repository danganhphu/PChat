using MediatR;
using PChat.Application.Bases;
using PChat.Application.Services;

namespace PChat.Application.Features.AuthFeatures.Commands.PostHubConnection;

public class PostHubConnectionHandler(IAuthService authService) : IRequestHandler<PostHubConnectionCommand, BaseResponse<string>>
{

    public async Task<BaseResponse<string>> Handle(PostHubConnectionCommand request, CancellationToken cancellationToken)
    {
        return await authService.PostHubConnection(request, cancellationToken);
    }
}