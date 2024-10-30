using Domain.Adapters;
using Domain.Repositories;

namespace Adapter.TmdbAdapter
{
	public class CheckInAdapter : ICheckInAdapter
	{
		private readonly ITicketRepository _ticketRepository;

		public CheckInAdapter(ITicketRepository ticketRepository)
		{
			_ticketRepository = ticketRepository;
		}

		public async Task<bool> CheckInAsync(int id)
		{
			var entity = new Domain.Entitys.Ticket();

			entity.Price = 20;
			entity.CreatedAt = DateTime.UtcNow;
			entity.MovieId = id;

			// Salva a atualização no repositório
			await _ticketRepository.CreateAsync(entity);

			return true;
		}
	}
}
