using Domain.Entitys;
using Domain.Models;

namespace Domain.Ports
{
    public interface IMoviePort
    {
        Task<IEnumerable<Movie>> GetAsync(Search search);
		Task<Movie?> GetByIdAsync(int id);

	}
}



