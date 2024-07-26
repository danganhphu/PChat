using PChat.Application.Services;
using PChat.Domain.Dto;
using MediatR;
using PChat.Application.Bases;
using PChat.Application.Features.AuthFeatures.Commands.Register;

namespace CleanArchitecture.Application.Features.AuthFeatures.Commands.Register;

public sealed class RegisterCommandHandler(IAuthService authService) : IRequestHandler<RegisterCommand, BaseResponse<RegisterResult>>
{
    public async Task<BaseResponse<RegisterResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        return  await authService.RegisterAsync(request);
    }
}
