using System.Net;
using Microsoft.Extensions.Localization;

namespace PChat.Application.Bases;

public class BaseResponseHandler(IStringLocalizer<BaseResponseHandler> localizer)
{

    public BaseResponse<T> Deleted<T>()
    {
        return new BaseResponse<T>()
        {
            StatusCode = HttpStatusCode.OK,
            Succeeded = true,
            Message = localizer["DeletedSuccessfully"]
        };
    }

    public BaseResponse<T> Success<T>(T entity, object meta = null)
    {
        return new BaseResponse<T>()
        {
            Data = entity,
            StatusCode = HttpStatusCode.OK,
            Succeeded = true,
            Message = localizer["Successfully"],
            Meta = meta
        };
    }

    public BaseResponse<T> Unauthorized<T>()
    {
        return new BaseResponse<T>()
        {
            StatusCode = HttpStatusCode.Unauthorized,
            Succeeded = true,
            Message = localizer["UnAuthorized"]
        };
    }

    public BaseResponse<T> BadRequest<T>(string message, List<string> errors = null)
    {
        return new BaseResponse<T>()
        {
            StatusCode = HttpStatusCode.BadRequest,
            Succeeded = false,
            Message = string.IsNullOrWhiteSpace(message) ? localizer["BadRequest"] : message,
            Errors = errors
        };
    }

    public BaseResponse<T> Conflict<T>(string message = null)
    {
        return new BaseResponse<T>()
        {
            StatusCode = HttpStatusCode.Conflict,
            Succeeded = false,
            Message = string.IsNullOrWhiteSpace(message) ? localizer["Conflict"] : message
        };
    }

    public BaseResponse<T> UnprocessableEntity<T>(string message = null)
    {
        return new BaseResponse<T>()
        {
            StatusCode = HttpStatusCode.UnprocessableEntity,
            Succeeded = false,
            Message = string.IsNullOrWhiteSpace(message) ? localizer["UnprocessableEntity"] : message
        };
    }

    public BaseResponse<T> NotFound<T>(string message = null)
    {
        return new BaseResponse<T>()
        {
            StatusCode = HttpStatusCode.NotFound,
            Succeeded = false,
            Message = string.IsNullOrWhiteSpace(message) ? localizer["NotFound"] : message
        };
    }

    public BaseResponse<T> Created<T>(T entity, object meta = null)
    {
        return new BaseResponse<T>()
        {
            Data = entity,
            StatusCode = HttpStatusCode.Created,
            Succeeded = true,
            Message = localizer["Created"],
            Meta = meta
        };
    }
}