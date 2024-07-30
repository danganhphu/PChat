using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PChat.Application.Abstractions.Data;
using PChat.Application.Exceptions;
using PChat.Domain.Entities;

namespace PChat.Application.Features.Users.Get;

internal sealed class GetUserQueryHandler(UserManager<User> userManager) : IRequestHandler<GetUserQuery, UserResponse>
{
    public async Task<UserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await userManager
            .Users
            .Where(p => p.Id == request.UserId.Value)
            .Select(p => new UserResponse(
                p.Id,
                p.Email,
                p.FullName,
                p.Phone))
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            throw new NotFoundException(nameof(user),request.UserId);
        }
        
        return user;
    }
}