using System.Net;
using System.Text.Json;
using backend.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace backend.Common.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger
        )
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DbUpdateException ex) when (ex.InnerException is PostgresException pxEx)
            {
                await HandlePostgresxceptionAsync(context, pxEx);
            }
            catch (ServiceException ex)
            {
                await HandleServicexceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "An unhandled exception occurred while processing the request."
                );
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandlePostgresxceptionAsync(HttpContext context, PostgresException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status409Conflict;
            return context.Response.WriteAsync(
                new ErrorData
                {
                    StatusCode = StatusCodes.Status409Conflict,
                    Message = exception.MessageText,
                    Detail = exception.ConstraintName,
                }.ToString()
            );
        }

        private Task HandleServicexceptionAsync(HttpContext context, ServiceException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception.StatusCode;
            return context.Response.WriteAsync(
                new ErrorData
                {
                    StatusCode = exception.StatusCode,
                    Message = exception.Message,
                }.ToString()
            );
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(
                new ErrorData
                {
                    StatusCode = context.Response.StatusCode,
                    Message = exception.Message + " " + exception.InnerException?.Message,
                }.ToString()
            );
        }

        private class ErrorData
        {
            public int StatusCode { get; set; }
            public string Message { get; set; } = string.Empty;
            public string? Detail { get; set; }

            public override string ToString()
            {
                return JsonSerializer.Serialize(this);
            }
        }
    }
}
