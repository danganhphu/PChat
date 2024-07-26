﻿using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using PChat.Application.Bases;

namespace PChat.Presentation.Abstraction;

[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
public class AppControllerBase : ControllerBase
{
    private IMediator? _mediatorInstance;
    protected IMediator Mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>() ?? throw new InvalidOperationException("IMediator service is not registered.");
    #region Actions

    public ObjectResult CustomResult<T>(BaseResponse<T> response)
    {
        return GetObjectResult(response);
    }
    
    public ObjectResult CustomResult<T>(BaseResponse<List<T>> response)
    {
        return GetObjectResult(response);
    }

    private ObjectResult GetObjectResult<T>(BaseResponse<T> response)
    {
        switch (response.StatusCode)
        {
            case HttpStatusCode.OK:
                return new OkObjectResult(response);
            case HttpStatusCode.Created:
                return new CreatedResult(string.Empty, response);
            case HttpStatusCode.Unauthorized:
                return new UnauthorizedObjectResult(response);
            case HttpStatusCode.BadRequest:
                return new BadRequestObjectResult(response);
            case HttpStatusCode.NotFound:
                return new NotFoundObjectResult(response);
            case HttpStatusCode.Accepted:
                return new AcceptedResult(string.Empty, response);
            case HttpStatusCode.UnprocessableEntity:
                return new UnprocessableEntityObjectResult(response);
            default:
                return new BadRequestObjectResult(response);
        }
    }

    #endregion
}