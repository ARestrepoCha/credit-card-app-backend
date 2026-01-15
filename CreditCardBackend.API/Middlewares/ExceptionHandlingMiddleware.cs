using CreditCardBackend.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace CreditCardBackend.API.Middlewares
{
    public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            logger.LogError(exception, "Exception: {Message}", exception.Message);

            var statusCode = exception switch
            {
                ValidationException => HttpStatusCode.BadRequest,
                UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                KeyNotFoundException => HttpStatusCode.NotFound,
                _ => HttpStatusCode.InternalServerError
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var problem = new ProblemDetails
            {
                Status = (int)statusCode,
                Title = GetTitle(exception),
                Detail = exception.Message,
                Instance = context.Request.Path
            };

            // Si es error de validación, inyectamos los detalles de los campos
            if (exception is ValidationException validationEx)
            {
                problem.Extensions.Add("errors", validationEx.Errors);
            }

            var json = JsonSerializer.Serialize(problem, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(json);
        }

        private static string GetTitle(Exception ex) => ex switch
        {
            ValidationException => "Error de validación",
            UnauthorizedAccessException => "No autorizado",
            KeyNotFoundException => "Recurso no encontrado",
            _ => "Error interno del servidor"
        };
    }
}
