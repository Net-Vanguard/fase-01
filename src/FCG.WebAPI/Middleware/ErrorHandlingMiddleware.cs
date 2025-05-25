using System.Net;
using System.Text.Json;

namespace FCG.WebAPI.Middleware
{
    /// <summary>
    /// Captura exceções não tratadas no pipeline e retorna um JSON padronizado.
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro não tratado durante a requisição");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            // Você pode customizar status codes com base no tipo de exceção
            // if (exception is MyNotFoundException) code = HttpStatusCode.NotFound;

            var problem = new
            {
                title = "Ocorreu um erro interno",
                status = (int)code,
                detail = exception.Message
            };

            var result = JsonSerializer.Serialize(problem);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }

    /// <summary>
    /// Extensão para registrar o middleware de erros.
    /// </summary>
    public static class ErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
