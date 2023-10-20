using CleanArchitecture.Api.MIddlewares.Errors;
using System.Text.Json;
using static CleanArchitecture.Api.MIddlewares.Errors.CodeErrorException;

namespace CleanArchitecture.Api.MIddlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var response = _env.IsDevelopment()
                    ? new CodeErrorException(StatusCodes.Status500InternalServerError, ex.Message, ex.StackTrace!)
                    : new CodeErrorException(StatusCodes.Status500InternalServerError);

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }

        }
    }
}
