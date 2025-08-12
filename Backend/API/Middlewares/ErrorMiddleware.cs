using System.Net;
using System.Text.Json;
using Application.Exceptions;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;

namespace API.Middlewares
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
                _logger.LogCritical(ex, "PostgresException occurred while processing the request.");
                await HandlePostgresxceptionAsync(context, pxEx);
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, "ServiceException occurred while processing the request.");
                await HandleServicexceptionAsync(context, ex);
            }
            catch (DomainException ex)
            {
                _logger.LogError(ex, "DomainException occurred while processing the request.");
                await HandleDomainExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(
                    ex,
                    "An unhandled exception occurred while processing the request."
                );
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandlePostgresxceptionAsync(
            HttpContext context,
            PostgresException exception
        )
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status409Conflict;

            return context.Response.WriteAsync(
                new ErrorData
                {
                    StatusCode = StatusCodes.Status409Conflict,
                    Message = exception.Message,
                    Detail = exception.ConstraintName,
                }.ToString()
            );
        }

        private static Task HandleServicexceptionAsync(
            HttpContext context,
            ServiceException exception
        )
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;
            int statusCode = 500;

            switch (exception)
            {
                case UnauthorizedException:
                    statusCode = StatusCodes.Status401Unauthorized;
                    context.Response.StatusCode = statusCode;
                    break;
            }
            return context.Response.WriteAsync(
                new ErrorData
                {
                    StatusCode = statusCode,
                    Message = exception.Message + " " + exception.InnerException?.Message,
                }.ToString()
            );
        }

        public static Task HandleDomainExceptionAsync(
            HttpContext context,
            DomainException exception
        )
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;
            int statusCode = 500;

            switch (exception)
            {
                case EntityNotFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    context.Response.StatusCode = statusCode;
                    break;
                case ConflictException:
                    statusCode = StatusCodes.Status409Conflict;
                    context.Response.StatusCode = statusCode;
                    break;
            }
            return context.Response.WriteAsync(
                new ErrorData
                {
                    StatusCode = statusCode,
                    Message = exception.Message + " " + exception.InnerException?.Message,
                }.ToString()
            );
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
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
