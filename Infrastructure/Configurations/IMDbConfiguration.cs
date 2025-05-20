using Domain.Ports;
using Infrastructure.Adapters;
using Infrastructure.Configurations.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace Infrastructure.Configurations
{
    public static class IMDbConfiguration
    {
        public static IServiceCollection AddIMDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IMDbAdapter>(x =>
            {
                var settings = configuration.GetConfiguration<IMDbConfigurationModel>();

                var apiKey = "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI0ZWQ5Y2E0NTUzZThiZmRmMjk5NjI1ZDI4ZjNlMGM0NCIsIm5iZiI6MTcyODQxODM3OS4zNzk5MjIsInN1YiI6IjY3MDU4Yjc1MDAwMDAwMDAwMDU4NTNiMiIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.p-MmF0K7-ku9kDlcyg4Ry8IeQMiufz5zTK-VT5wuOu8";
                var bearerToken = $"Bearer {apiKey}";
                x.DefaultRequestHeaders.Add("Authorization", bearerToken);
                x.DefaultRequestHeaders.Add("accept", "application/json");

                x.BaseAddress = new Uri(settings.Url);
            });

            services.AddScoped<IMoviePort, IMDbAdapter>();

            return services;
        }
    }
}
