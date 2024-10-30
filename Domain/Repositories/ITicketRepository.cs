using Domain.Entitys;

namespace Domain.Repositories
{
	public interface ITicketRepository
	{
		Task<Ticket> GetByIdAsync(string id);
		Task<IEnumerable<Ticket>> GetAllAsync();
		Task CreateAsync(Ticket entity);
		Task UpdateAsync(Ticket entity);
		Task DeleteAsync(string id);
	}
}
