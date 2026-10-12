using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CinemaWeb
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext, Exception exception, CancellationToken ct)
        {
            _logger.LogError(exception, "Ngoại lệ: {Msg}", exception.Message);

            var problemDetails = new ProblemDetails
            {
                Instance = httpContext.Request.Path,
                Status = exception switch
                {
                    KeyNotFoundException => StatusCodes.Status404NotFound,
                    ArgumentException or InvalidOperationException => StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status500InternalServerError
                },
                Title = exception switch
                {
                    KeyNotFoundException => "Không tìm thấy tài nguyên",
                    ArgumentException or InvalidOperationException => "Yêu cầu không hợp lệ",
                    _ => "Lỗi máy chủ nội bộ"
                },
                Detail = exception.Message
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, ct);
            return true; // Xác nhận đã xử lý xong ngoại lệ
        }
    }
}
