using Domain.Entitys;
using Domain.Models;

namespace Domain.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetAsync(Search pesquisa);
        Task<Movie> GetByIdAsync(int id);
    }
}
