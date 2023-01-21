using FizzBuzz.Application.Contracts.FileStorage;
using FizzBuzz.Application.Contracts.Persistence;
using FizzBuzz.Infrastructure.FileStorage;
using FizzBuzz.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FizzBuzz.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Repositories
        services.AddScoped<IFileRepository, FileRepository>();

        // External file storage
        services.AddSingleton<IMultiThreadFileWriter, MultiThreadFileWriter>();

        return services;
    }
}