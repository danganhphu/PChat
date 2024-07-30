using MediatR;
using PChat.Application.Services;

namespace PChat.Application.Features.AuthFeatures.Commands.Register;

public sealed class RegisterCommandHandler(IAuthService authService) : IRequestHandler<RegisterCommand, RegisterResponse>
{
    public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        return await authService.RegisterAsync(request);
    }
}
