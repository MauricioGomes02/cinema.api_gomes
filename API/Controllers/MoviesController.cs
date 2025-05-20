using AutoMapper;
using Domain.Models;
using Domain.Services;
using FilmeAPI.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Filme.API.Controllers
{
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IFilmesService _movieService;

        public MoviesController(IFilmesService filmesService)
        {
            this._movieService = filmesService ?? throw new ArgumentNullException(nameof(filmesService));
        }

        [HttpGet]
        [ProducesResponseType(typeof(MovieGetResult), 200)]
        public async Task<IActionResult> GetFilmesAsync(
            [FromQuery] MovieGet filmesGet)
        {
            Pesquisa pesquisa = mapper.Map<MovieGet, Pesquisa>(filmesGet);
            IEnumerable<Filme> filmes = await _movieService
                .ObterFilmesAsync(pesquisa);
            IEnumerable<MovieGetResult> filmesGetResults =
                mapper.Map<IEnumerable<MovieGetResult>>(filmes);

            return Ok(filmesGetResults);
        }

        [HttpPost("check-in")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CheckInFilmeAsync([FromQuery] int filmeId)
        {
            try
            {
                // Verifica se o ID do filme é válido
                var filme = await _movieService.ObterFilmesPorIdAsync(filmeId);
                if (filme == null)
                {
                    return NotFound($"Filme com Id = {filmeId} não foi encontrado.");
                }

                // Realiza o check-in do filme
                var sucesso = await _movieService.CheckInFilmeAsync(filmeId);
                if (sucesso)
                {
                    return Ok($"Check-in realizado com sucesso para o filme de Id = {filmeId}.");
                }

                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Falha ao realizar o check-in para o filme de Id = {filmeId}.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro interno: {ex.Message}");
            }
        }

    }
}
