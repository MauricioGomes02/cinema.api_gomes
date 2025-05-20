using Domain.Adapters;
using Domain.Models;
using Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Xunit;
using Assert = Xunit.Assert;

namespace Application.Tests
{
    [TestClass]
    public class FilmesTests
    {
        private readonly IMovieService filmesService;
        private readonly Mock<ITmdbAdapter> tmdbAdapterMock;
		private readonly Mock<ICheckInAdapter> checkInAdapterMock;

		public FilmesTests()
        {
            tmdbAdapterMock = new Mock<ITmdbAdapter>();
			checkInAdapterMock = new Mock<ICheckInAdapter>();

			filmesService = new FilmesService(tmdbAdapterMock.Object, checkInAdapterMock.Object);
        }

        [TestMethod]
        [Trait(nameof(IMovieService.GetAsync), "Sucesso")]
        public async Task ObterFilmesAsync_Sucesso()
        {
            // Objeto que sera utilizado para retorno do Mock
            var expected = new List<Filme>()
                {
                    new Filme()
                    {
                        Id = 10447, 
                        Descricao = "descricao_teste",
                        Nome = "nome_teste"
                    }
                };

            tmdbAdapterMock
                .Setup(m => m.GetFilmesAsync(It.IsAny<Search>()))
                .ReturnsAsync(expected);

            var filmes = await filmesService.ObterFilmesAsync(new Search()
            {
                SearchTerm = "teste"
            });

            var exepectedSingle = expected.Single();

            Assert.Contains(filmes, f =>
                    f.Id == exepectedSingle.Id &&
                    f.Descricao == exepectedSingle.Descricao &&
                    f.Nome == exepectedSingle.Nome);
        }

        [TestMethod]
        [Trait(nameof(IMovieService.GetAsync), "Erro")]
        public async Task ObterFilmesAsync_Erro()
        {
            await Assert.ThrowsAnyAsync<Exception>(async () =>
            {
                await filmesService.ObterFilmesAsync(new Search());
            });
        }
    }
}
