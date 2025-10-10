using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MindHive.Application.ApiServiceInterfaces;
using MindHive.Domain.Repositories;
using MindHive.Infrastructure.Data;
using MindHive.Infrastructure.Repositories;

namespace MindHive.Infrastructure.DependencyInjection;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MindHiveDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
//addRepositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IErrorLogRepository, ErrorLogRepository>();

        return services;
    }
}