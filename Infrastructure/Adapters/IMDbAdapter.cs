using Domain.Entitys;
using Domain.Models;
using Domain.Ports;
using Infrastructure.Configurations;
using Infrastructure.Configurations.Models;
using Infrastructure.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Infrastructure.Adapters
{
    public class IMDbAdapter : IMoviePort
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public IMDbAdapter(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient(nameof(IMDbAdapter));
            _configuration = configuration;
        }
        public async Task<IEnumerable<Movie>> GetAsync(Search search)
        {
            var IMDbConfiguration = _configuration.GetConfiguration<IMDbConfigurationModel>();
            var query = $"?query={search.SearchTerm}&api_key={IMDbConfiguration.ApiKey}&language={IMDbConfiguration.Language}&year={search.ReleaseYear}";

            var httpResponseMessage = await _httpClient.GetAsync($"3/search/movie{query}").ConfigureAwait(false);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new Exception("Unable to get the movies");
            }

            var content = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            var movies = JsonConvert.DeserializeObject<GetMoviesResult>(content);

            return movies?.Results.Select(x => new Movie 
            { 
                Id = x.Id, 
                Name = x.Title, 
                Description = x.Overview, 
                ReleaseDate = x.ReleaseDate 
            }) ?? Enumerable.Empty<Movie>();
        }

        public async Task<Movie?> GetByIdAsync(int id)
        {
            var httpResponseMessage = await _httpClient.GetAsync($"3/movie/{id}").ConfigureAwait(false);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new Exception("Unable to get the movie");
            }

            var content = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            var movie = JsonConvert.DeserializeObject<GetMoviesItemResult>(content);
            return new Movie
            {
                Id = movie.Id,
                Name = movie.Title,
                Description = movie.Overview,
                ReleaseDate = movie.ReleaseDate
            };
        }
    }
}
