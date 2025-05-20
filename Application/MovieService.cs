using Domain.Entitys;
using Domain.Models;
using Domain.Ports;
using Domain.Services;

namespace Application
{
    public class MovieService : IMovieService
    {
        private readonly IMoviePort _movieAdapter;

        public MovieService(IMoviePort movieAdapter)
        {
            this._movieAdapter = movieAdapter ?? throw new ArgumentNullException(nameof(movieAdapter));
        }

        public async Task<IEnumerable<Movie>> GetAsync(Search search)
        {
            if (search == null || string.IsNullOrWhiteSpace(search.SearchTerm))
            {
                throw new Exception("Search criteria are not valid");
            }

            return await _movieAdapter.GetAsync(search).ConfigureAwait(false);
        }

        public async Task<Movie> GetByIdAsync(int id)
        {
            if (id < 0)
            {
                throw new Exception("Search criteria are not valid");
            }

            return await _movieAdapter.GetByIdAsync(id).ConfigureAwait(false);
        }

    }
}
