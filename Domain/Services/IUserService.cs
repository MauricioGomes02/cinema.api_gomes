using Domain.Entitys;
using Domain.Models;

namespace Domain.Services
{
    public interface IUserService
    {
        Task<User> AddAsync(CreateUser createUser);
        Task<User?> GetAsync(Guid id);
        Task UpdateAsync(User user);
        Task<User?> DeleteAsync(Guid id);
    }
}
