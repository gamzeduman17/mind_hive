using Microsoft.Extensions.DependencyInjection;
using MindHive.Application.ApiServiceInterfaces;
using MindHive.Application.ApiServices;

namespace MindHive.Infrastructure.DependencyInjection;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        //Add all service here
        services.AddScoped<IUserService, UserService>();
        services.AddSingleton<IJwtService, JwtService>();
        services.AddScoped<IErrorLogService, ErrorLogService>();


        return services;
    }
}