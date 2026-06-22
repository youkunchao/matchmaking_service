using Microsoft.AspNetCore.Mvc;
using Zhenghao.Matchmaking.Api.Middleware;

namespace Zhenghao.Matchmaking.Api.Extensions;

public static class ServiceExtensions
{
    public static void AddCustomValidation(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var errors = context.ModelState
                    .Where(e => e.Value?.Errors.Any() ?? false)
                    .SelectMany(e => e.Value!.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var result = new
                {
                    Code = -2,
                    Message = "请求参数验证失败",
                    Errors = errors
                };

                return new BadRequestObjectResult(result);
            };
        });
    }

    public static void UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<GlobalExceptionMiddleware>();
    }
}