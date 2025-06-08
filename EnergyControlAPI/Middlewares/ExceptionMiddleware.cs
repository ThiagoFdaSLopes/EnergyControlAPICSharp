using System.ComponentModel.DataAnnotations;

namespace EnergyControlAPI.Middlewares
{
    // Middlewares/ExceptionMiddleware.cs
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _log;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> log)
        {
            _next = next; _log = log;
        }

        public async Task Invoke(HttpContext ctx)
        {
            try
            {
                await _next(ctx);
            }
            catch (ValidationException vex)
            {
                ctx.Response.StatusCode = StatusCodes.Status400BadRequest;
                await ctx.Response.WriteAsJsonAsync(new { Error = vex.Message });
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Unhandled error");
                ctx.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await ctx.Response.WriteAsJsonAsync(new { Error = "Erro interno no servidor." });
            }
        }
    }

}
