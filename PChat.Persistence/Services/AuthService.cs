using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PChat.Application.Abstractions.JWT;
using PChat.Application.Constants;
using PChat.Application.Exceptions;
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
    IJwtProvider jwtProvider,
    ICurrentUser currentUser)
    : IAuthService
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

    public async Task<LoginCommandResponse> LoginAsync(LoginCommand request, CancellationToken cancellationToken)
    {
        var user =
            await userManager.Users
                .Where(
                    p => p.UserName == request.UserNameOrEmail
                         || p.Email == request.UserNameOrEmail)
                .FirstOrDefaultAsync(cancellationToken);

        if (user == null) throw new Exception("User not found!");

        var result = await userManager.CheckPasswordAsync(user, request.Password);

        if (result)
        {
            LoginCommandResponse response = await jwtProvider.CreateTokenAsync(user);
            return response;
        }

        throw new Exception("Incorrect password!");
    }

    public async Task<RegisterResponse> RegisterAsync(RegisterCommand request)
    {
        var user = mapper.Map<User>(request);
        user.Avatar = Constants.AvatarDefault;

        var existingUser =
            await userManager.Users
                .Where(
                    p => p.UserName == request.UserName
                         || p.Email == request.Email)
                .FirstOrDefaultAsync();

        if (existingUser != null)
        {
            throw new Exception($"Username {request.UserName} already exists");
        }

        var result = await userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            return new RegisterResponse(user.Id);
        }
        else
        {
            StringBuilder str = new StringBuilder();
            foreach (var err in result.Errors)
            {
                str.AppendFormat("•{0}\n", err.Description);
            }

            throw new BadRequestException($"{str}");
        }
    }

    public async Task<string> PostHubConnection(PostHubConnectionCommand request, CancellationToken cancellationToken)
    {
        var userId = currentUser.Id;
        var user = await userManager.Users
            .FirstOrDefaultAsync(x => x.Id.Equals(userId), cancellationToken);

        if (user != null)
        {
            user.CurrentSession = request.Key;
            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return "PostHubConnection Success";
            }
            else
            {
                StringBuilder str = new StringBuilder();
                foreach (var err in result.Errors)
                {
                    str.AppendFormat("•{0}\n", err.Description);
                }

                throw new BadRequestException($"{str}");
            }
        }
        else
        {
            throw new Exception($"PostHubConnection key {request.Key} null");
        }
    }
}