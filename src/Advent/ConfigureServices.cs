using Microsoft.Extensions.Configuration;

using Advent.Console.Infrastructure.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddConsoleServices(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<InputOptions>(config.GetSection(InputOptions.Name));

        services.AddSingleton<IInputService, InputService>();
        services.AddSingleton<ISolutionFactory, SolutionFactory>();

        return services;
    }
}
