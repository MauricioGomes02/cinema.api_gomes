using Domain.Adapters;
using Domain.Models;
using Domain.Services;

namespace Application
{
    public class FilmesService : IFilmesService
    {
        private readonly ITmdbAdapter tmdbAdapter;
        private readonly ICheckInAdapter checkInAdapter;

		public FilmesService(ITmdbAdapter tmdbAdapter, ICheckInAdapter checkInAdapter)
        {
            this.tmdbAdapter = tmdbAdapter ?? throw new ArgumentNullException(nameof(tmdbAdapter));
            this.checkInAdapter = checkInAdapter ?? throw new ArgumentNullException(nameof(checkInAdapter));
        }

		public async Task<bool> CheckInFilmeAsync(int movieId)
		{
            return await this.checkInAdapter.CheckInAsync(movieId);
		}

		public async Task<IEnumerable<Filme>> ObterFilmesAsync(
            Pesquisa pesquisa)
        {
            if (pesquisa == null || string.IsNullOrWhiteSpace(pesquisa.TermoPesquisa))
            {
                throw new Exception("Critérios de pesquisa não são validos.");
            }

            IEnumerable<Filme> resultado = await tmdbAdapter
                .GetFilmesAsync(pesquisa);

            return resultado;
        }

        public async Task<Movie> ObterFilmesPorIdAsync(int movie_id)
        {
            if (movie_id < 0)
            {
                throw new Exception("Critérios de pesquisa não são validos.");
            }

            var resultado = await tmdbAdapter
                .GetFilmesPorIdAsync(movie_id);

            return resultado;
        }

    }
}
