using MediatR;
using Microsoft.Extensions.Localization;
using PChat.Application.Abstractions.Data;
using PChat.Application.Exceptions;
using PChat.Domain.Interface;

namespace PChat.Application.Features.Users.Delete;

internal sealed class DeleteUserCommandHandler(IUserRepository productRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteUserCommand>
{
    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await productRepository.GetByIdAsync(request.UserId);

        if (user is null)
        {
            throw new NotFoundException(nameof(user),request.UserId);
        }

        productRepository.Remove(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
