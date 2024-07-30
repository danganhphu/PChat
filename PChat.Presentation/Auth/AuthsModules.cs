using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using PChat.Application.Features.AuthFeatures.Commands.Register;

namespace PChat.Presentation.Auth;

public class AuthsModules: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/auths");

        group.MapPost("/register", Register);
    }

    private static async Task<IResult> Register(
        RegisterRequest request,
        ISender sender)
    {
        var command = new RegisterCommand(
            request.Email,
            request.UserName,
            request.FullName,
            request.Password);

        await sender.Send(command);

        return Results.Ok();
    }
}