using PChat.Application.Services;
using PChat.Domain.Dto;
using MediatR;
using PChat.Application.Features.AuthFeatures.Commands.Register;

namespace CleanArchitecture.Application.Features.AuthFeatures.Commands.Register;

public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, MessageResponse>
{
    private readonly IAuthService _authService;

    public RegisterCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<MessageResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        await _authService.RegisterAsync(request);
        return new("Kullanıcı kaydı başarıyla tamamlandı!");
    }
}
