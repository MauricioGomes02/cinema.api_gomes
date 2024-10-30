using Domain.Entitys;
using Domain.Repositories;
using MongoDB.Driver;

namespace IMDbAdapter.Mongo
{
	internal class TicketRepository : ITicketRepository
	{
		private readonly IMongoCollection<Ticket> _collection;

		public TicketRepository(IMongoDatabase database)
		{
			_collection = database.GetCollection<Ticket>("tickets");
		}

		public async Task<Ticket> GetByIdAsync(string id)
		{
			return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
		}

		public async Task<IEnumerable<Ticket>> GetAllAsync()
		{
			return await _collection.Find(_ => true).ToListAsync();
		}

		public async Task CreateAsync(Ticket entity)
		{
			await _collection.InsertOneAsync(entity);
		}

		public async Task UpdateAsync(Ticket entity)
		{
			await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
		}

		public async Task DeleteAsync(string id)
		{
			await _collection.DeleteOneAsync(x => x.Id == id);
		}
	}
}
