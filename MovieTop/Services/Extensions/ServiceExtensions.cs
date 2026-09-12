using Microsoft.Extensions.DependencyInjection;
using MovieTop.Services.Interfaces;

namespace MovieTop.Services.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddMovieServices(this IServiceCollection services)
    {
        services.AddScoped<IMovieService, MovieService>();

        return services;
    }
}