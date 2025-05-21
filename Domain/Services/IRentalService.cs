using Domain.Entitys;
using Domain.Models;

namespace Domain.Services
{
    public interface IRentalService
    {
        Task<Rental> AddAsync(CreateRental createRental);
        Task<IEnumerable<Rental>> GetAllAsync();
        Task<Rental?> GetByIdAsync(Guid id);
        Task UpdateAsync(Rental user);
    }
}
