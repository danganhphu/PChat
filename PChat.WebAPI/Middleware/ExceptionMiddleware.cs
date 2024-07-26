using System.Net;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PChat.Application.Bases;
using PChat.Domain.Entities;
using PChat.Persistance.Context;
using System.Text.Json;


namespace PChat.WebAPI.Middleware;

public sealed class ExceptionMiddleware : IMiddleware
{
    private readonly AppDbContext _context;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception ex)
        {
            // await LogExceptionToDatabaseAsync(ex, httpContext.Request);
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        //_logger.LogError(error, "An unexpected error occurred.");

        var response = context.Response;
        response.ContentType = "application/json";
        var responseModel = new BaseResponse<string>() { Succeeded = false, Message = ex?.Message };
        //TODO:: cover all validation errors
        switch (ex)
        {
            case UnauthorizedAccessException e:
                // custom application error
                responseModel.Message = ex.Message;
                responseModel.StatusCode = HttpStatusCode.Unauthorized;
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                break;

            case ValidationException e:
                // custom validation error
                responseModel.Message = ex.Message;
                responseModel.StatusCode = HttpStatusCode.UnprocessableEntity;
                response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                break;
            case KeyNotFoundException e:
                // not found error
                responseModel.Message = ex.Message;
                ;
                responseModel.StatusCode = HttpStatusCode.NotFound;
                response.StatusCode = (int)HttpStatusCode.NotFound;
                break;

            case DbUpdateException e:
                // can't update error
                responseModel.Message = e.Message;
                responseModel.StatusCode = HttpStatusCode.BadRequest;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
            
            default:
                responseModel.Message = ex.Message;
                responseModel.StatusCode = HttpStatusCode.InternalServerError;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }

        var result = JsonSerializer.Serialize(responseModel);

        await response.WriteAsync(result);
        LogAdditionalInfo(ex, responseModel);
    }
    private void LogAdditionalInfo(Exception error, BaseResponse<string> responseModel)
    {
        _logger.LogInformation($"Error Message: {error.Message}");

        if (error.InnerException != null)
        {
            _logger.LogInformation($"Inner Exception: {error.InnerException.Message}");
        }
    }
    // private async Task LogExceptionToDatabaseAsync(Exception ex, HttpRequest request)
    // {
    //     ErrorLog errorLog = new()
    //     {
    //         ErrorMessage = ex.Message,
    //         StackTrace = ex.StackTrace,
    //         RequestPath = request.Path,
    //         RequestMethod = request.Method,
    //         Timestamp = DateTime.Now,
    //     };
    //
    //     await _context.Set<ErrorLog>().AddAsync(errorLog, default);
    //     await _context.SaveChangesAsync(default);
    // }
}