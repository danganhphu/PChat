using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PChat.Application.Services;
using PChat.Domain.Entities;

namespace PChat.Persistence.Services;

public class CurrentUser(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager) : IUser
{
    public string? Id => httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
}