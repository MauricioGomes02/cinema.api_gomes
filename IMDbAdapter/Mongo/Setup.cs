using Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;

namespace IMDbAdapter.Mongo
{
	[ExcludeFromCodeCoverage]
	public static class Setup
	{
		public static IServiceCollection AddMongo(this IServiceCollection services, MongoDbAdpterConfiguration mongoDbAdpterConfiguration)
		{
			// Configurar o MongoDB Client no IoC
			services.AddSingleton<IMongoClient, MongoClient>(sp =>
			{
				var connectionString = mongoDbAdpterConfiguration.DefaultConnection;
				var settings = MongoClientSettings.FromConnectionString(connectionString);
				settings.ServerApi = new ServerApi(ServerApiVersion.V1);
				return new MongoClient(settings);
			});

			// Configurar a injeção de dependência para acessar o banco de dados
			services.AddScoped(sp =>
			{
				var client = sp.GetRequiredService<IMongoClient>();
				return client.GetDatabase("cinema");
			});

			services.AddScoped<ITicketRepository, TicketRepository>();

			return services;
		}
	}
}
