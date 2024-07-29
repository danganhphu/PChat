using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PChat.Application.Features.AuthFeatures.Commands.CreateNewTokenByRefreshToken;
using PChat.Application.Features.AuthFeatures.Commands.Login;
using PChat.Application.Features.AuthFeatures.Commands.PostHubConnection;
using PChat.Application.Features.AuthFeatures.Commands.Register;
using PChat.Domain.Dto;
using PChat.Presentation.Abstraction;
using PChat.Presentation.Configurations;

namespace PChat.Presentation.Controllers;

public sealed class AuthController : AppControllerBase
{
    
    [HttpPost(Router.AuthRouting.Actions.Register)]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterCommand request, CancellationToken cancellationToken)
    {
        return CustomResult(await Mediator.Send(request, cancellationToken));
    }

    [HttpPost(Router.AuthRouting.Actions.Login)]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginCommand request, CancellationToken cancellationToken)
    {
        return CustomResult(await Mediator.Send(request, cancellationToken));
    }

    [HttpPost(Router.AuthRouting.Actions.CreateTokenByRefreshToken)]
    public async Task<IActionResult> CreateTokenByRefreshToken(CreateNewTokenByRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        LoginCommandResponse response = await Mediator.Send(request, cancellationToken);
        return Ok(response);
    }
    
    [HttpPost(Router.AuthRouting.Actions.PostHubconnection)]
    public async Task<IActionResult> PostHubConnection(PostHubConnectionCommand request, CancellationToken cancellationToken)
    {
        return CustomResult(await Mediator.Send(request, cancellationToken));
    }
}
