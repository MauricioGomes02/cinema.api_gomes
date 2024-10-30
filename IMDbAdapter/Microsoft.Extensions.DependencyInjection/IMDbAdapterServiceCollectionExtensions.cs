using Adapter.TmdbAdapter;
using Domain.Adapters;
using Refit;
using System.Diagnostics.CodeAnalysis;
using TmdbAdapter.Clients;

namespace Microsoft.Extensions.DependencyInjection.IMDbAdapter
{
	public static class IMDbAdapterServiceCollectionExtensions
	{
		[ExcludeFromCodeCoverage]
		public static IServiceCollection AddIMDbAdapter(
			this IServiceCollection services,
			TmdbAdapterConfiguration tmdbAdapterConfiguration)
		{
			if (services == null)
			{
				throw new ArgumentNullException(nameof(services));
			}

			if (tmdbAdapterConfiguration == null)
			{
				throw new ArgumentNullException(nameof(tmdbAdapterConfiguration));
			}

			services.AddSingleton(tmdbAdapterConfiguration);

			services.AddScoped(serviceProvider =>
			{
				// Obter o token de uma variável de ambiente ou configuração segura
				var token = Environment.GetEnvironmentVariable("TMDB_API_TOKEN")
							?? "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI0ZWQ5Y2E0NTUzZThiZmRmMjk5NjI1ZDI4ZjNlMGM0NCIsIm5iZiI6MTcyODQxODM3OS4zNzk5MjIsInN1YiI6IjY3MDU4Yjc1MDAwMDAwMDAwMDU4NTNiMiIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.p-MmF0K7-ku9kDlcyg4Ry8IeQMiufz5zTK-VT5wuOu8";

				// Configurar o HttpClient com cabeçalho Authorization
				var httpClient = new HttpClient
				{
					BaseAddress = new Uri("https://api.themoviedb.org/3")
				};
				httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
				httpClient.DefaultRequestHeaders.Add("accept", "application/json");

				// Criação do cliente Refit usando o HttpClient configurado
				var tmdbApi = RestService.For<ITmdbApi>(httpClient);

				return tmdbApi;

			});

			services.AddScoped<ITmdbAdapter, Adapter.TmdbAdapter.TmdbAdapter>();
			services.AddScoped<ICheckInAdapter, CheckInAdapter>();

			return services;
		}
	}
}
