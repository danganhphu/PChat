using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PChat.Application.Abstractions.JWT;
using PChat.Application.Features.AuthFeatures.Commands.Login;
using PChat.Domain.Entities;

namespace PChat.Infrastructure.Authenticaton;

public sealed class JwtProvider(IOptions<JwtOptions> jwtOptions, UserManager<User> userManager) : IJwtProvider
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;

    public async Task<LoginCommandResponse> CreateTokenAsync(User user)
    {
        var claims = new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Name, user.UserName),
            new Claim("FullName",user.FullName)
        };

        DateTime expires = DateTime.Now.AddHours(1);

        JwtSecurityToken jwtSecurityToken = new(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            notBefore: DateTime.Now,
            expires: expires,
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),SecurityAlgorithms.HmacSha256));

        string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        string refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpires = expires.AddMinutes(15);
        await userManager.UpdateAsync(user);

        LoginCommandResponse response = new(
            token,
            refreshToken,
            user.RefreshTokenExpires,
            user.Id);

        return response;
    }
}
