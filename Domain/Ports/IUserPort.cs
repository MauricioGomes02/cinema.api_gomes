using Domain.Entitys;

namespace Domain.Ports
{
    public interface IUserPort
    {
        Task<User> AddAsync(User user);
        Task<User?> GetAsync(Guid id);
        Task UpdateAsync(User user);
    }
}
