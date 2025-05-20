using Domain.Entitys;
using Domain.Models;
using Domain.Services;
using Filme.API.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Filme.API.Controllers
{
    [Route("movies")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService filmesService)
        {
            this._movieService = filmesService ?? throw new ArgumentNullException(nameof(filmesService));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Movie>), 200)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync(
            [FromQuery] MovieGet moviesGet)
        {
            var search = new Search
            {
                SearchTerm = moviesGet.SearchTerm,
                ReleaseYear = moviesGet.ReleaseYear
            };

            try
            {
                var movies = await _movieService.GetAsync(search);

                return Ok(movies);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Movie), 200)]
        [ProducesResponseType (StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var movie = await _movieService.GetByIdAsync(id);

                return Ok(movie);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
