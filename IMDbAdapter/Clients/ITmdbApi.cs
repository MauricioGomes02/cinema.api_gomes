using Domain.Models;
using Refit;

namespace TmdbAdapter.Clients
{
    internal interface ITmdbApi
    {
        [Get("/search/movie")]
        Task<TmdbSearchMoviesGetResult> SearchMovies([Query] TmdbSearchMoviesGet tmdbSearchMoviesGet);

        [Get("/movie/{movie_id}")]
		Task<Movie> SearchMoviesById([Query] int movie_id);
	}
}

