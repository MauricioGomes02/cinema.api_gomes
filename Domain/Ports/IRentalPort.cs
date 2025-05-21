using Domain.Entitys;

namespace Domain.Ports
{
    public interface IRentalPort
    {
        Task<Rental> AddAsync(Rental user);
        Task<IEnumerable<Rental>> GetAllAsync();
        Task<Rental?> GetByIdAsync(Guid id);
        Task UpdateAsync(Rental user);
        Task<User> AddUserAsync(User user);
        Task<User?> GetUserAsync(Guid id);
        Task UpdateUserAsync(User user);
        Task<bool> ExistsMovieAsync(int movieId);
    }
}
