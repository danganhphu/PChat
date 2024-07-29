using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using PChat.Application.Abstractions;
using PChat.Application.Bases;
using PChat.Application.Constants;
using PChat.Application.Features.AuthFeatures.Commands.CreateNewTokenByRefreshToken;
using PChat.Application.Features.AuthFeatures.Commands.Login;
using PChat.Application.Features.AuthFeatures.Commands.PostHubConnection;
using PChat.Application.Features.AuthFeatures.Commands.Register;
using PChat.Application.Services;
using PChat.Domain.Entities;

namespace PChat.Persistence.Services;

public sealed class AuthService(
    UserManager<User> userManager,
    IMapper mapper,
    IStringLocalizer<BaseResponseHandler> localizer,
    IStringLocalizer<AuthService> authServiceLocalizer,
    IJwtProvider jwtProvider,
    IUser currentUser)
    : BaseResponseHandler(localizer), IAuthService
{
    public async Task<LoginCommandResponse> CreateTokenByRefreshTokenAsync(CreateNewTokenByRefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        User user = await userManager.FindByIdAsync(request.UserId);
        if (user == null) throw new Exception("User not found!");

        if (user.RefreshToken != request.RefreshToken)
            throw new Exception("Refresh Token is invalid!");

        if (user.RefreshTokenExpires < DateTime.Now)
            throw new Exception("Refresh Token has expired!");


        LoginCommandResponse response = await jwtProvider.CreateTokenAsync(user);
        return response;
    }

    public async Task<BaseResponse<LoginCommandResponse>> LoginAsync(LoginCommand request, CancellationToken cancellationToken)
    {
        User? user =
            await userManager.Users
                .Where(
                    p => p.UserName == request.UserNameOrEmail
                         || p.Email == request.UserNameOrEmail)
                .FirstOrDefaultAsync(cancellationToken);
        
        if (user == null)
        {
            return NotFound<LoginCommandResponse>(localizer["UserNotFound"]);
        }

        var result = await userManager.CheckPasswordAsync(user, request.Password);
        
        if (!result)
        {
            return NotFound<LoginCommandResponse>(localizer["InvalidCredentials", request.UserNameOrEmail]);
        }

        var response =  await jwtProvider.CreateTokenAsync(user);
        return Success(response);
    }

    public async Task<BaseResponse<RegisterResult>> RegisterAsync(RegisterCommand request)
    {
        var user = mapper.Map<User>(request);
        user.Avatar = Constants.AvatarDefault;
        var existingUser = await userManager.FindByNameAsync(request.UserName);

        if (existingUser != null)
        {
            return Conflict<RegisterResult>(authServiceLocalizer["UsernameExists", request.UserName]);
        }
        
        var existingEmail = await userManager.FindByEmailAsync(request.Email);

        if (existingEmail == null)
        {
            var result = await userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                return Created(new RegisterResult(user.Id));
            }
            else
            {
                return BadRequest<RegisterResult>(authServiceLocalizer["BadRequestDetails"], result.Errors.Select(a => a.Description).ToList());
            }
        }
        else
        {
            return Conflict<RegisterResult>(authServiceLocalizer["EmailExists", request.Email]);
        }
    }
    
    public async Task<BaseResponse<string>> PostHubConnection(PostHubConnectionCommand request, CancellationToken cancellationToken)
    {
        var userId = currentUser.Id;
        var user = await userManager.Users
            .FirstOrDefaultAsync(x => x.Id.Equals(userId), cancellationToken);

        if (user != null)
        {
            user.CurrentSession = request.Key;
            var result = await userManager.UpdateAsync(user);
            
            return !result.Succeeded ? BadRequest<string>(authServiceLocalizer["BadRequestDetails"], result.Errors.Select(a => a.Description).ToList()) : Success("PostHubConnection Success");
        }
        else
        {
            return NotFound<string>(localizer["UserNotFoundHubConnection", request.Key]);
        }
    }
}