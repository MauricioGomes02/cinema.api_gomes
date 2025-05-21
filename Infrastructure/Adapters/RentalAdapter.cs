using Domain.Entitys;
using Domain.Ports;
using Infrastructure.Configurations.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Adapters
{
    public class RentalAdapter : IRentalPort
    {
        private readonly IMongoCollection<User> _collectionUser;
        private readonly IMongoCollection<Rental> _collection;
        private readonly IMoviePort _movieAdapter;

        public RentalAdapter(
            IOptions<MongoDbAdapterConfigurationModel> configuration, 
            IMongoClient client,
            IMoviePort movieAdapter)
        {
            var database = client.GetDatabase(configuration.Value.DatabaseName);
            _collectionUser = database.GetCollection<User>("rental_user");
            _collection = database.GetCollection<Rental>(configuration.Value.CollectionName);
            _movieAdapter = movieAdapter;
        }

        public async Task<User> AddUserAsync(User user)
        {
            await _collectionUser.InsertOneAsync(user).ConfigureAwait(false);
            return user;
        }

        public async Task<User?> GetUserAsync(Guid id)
        {
            var cursor = await _collectionUser.FindAsync(x => x.Id == id);
            return cursor.FirstOrDefault();
        }

        public async Task UpdateUserAsync(User user)
        {
            var updateDefinition = new UpdateDefinitionBuilder<User>()
                .Set(x => x.Name, user.Name)
                .Set(x => x.Active, user.Active);

            await _collectionUser.UpdateOneAsync(x => x.Id == user.Id, updateDefinition);
        }
        public async Task<Rental> AddAsync(Rental rental)
        {
            await _collection.InsertOneAsync(rental).ConfigureAwait(false);
            return rental;
        }

        public async Task<bool> ExistsMovieAsync(int movieId)
        {
            return await _movieAdapter.GetByIdAsync(movieId).ConfigureAwait(false) is not null;
        }

        public async Task<Rental?> GetByIdAsync(Guid id)
        {
            var cursor = await _collection.FindAsync(x => x.Id == id);
            return cursor.FirstOrDefault();
        }

        public async Task UpdateAsync(Rental rental)
        {
            var updateDefinition = new UpdateDefinitionBuilder<Rental>()
                .Set(x => x.ReturnDate, rental.ReturnDate)
                .Set(x => x.Amount, rental.Amount);

            await _collection.UpdateOneAsync(x => x.Id == rental.Id, updateDefinition);
        }

        public async Task<IEnumerable<Rental>> GetAllAsync()
        {
            var cursor = await _collection.FindAsync(x => true).ConfigureAwait(false);
            return await cursor.ToListAsync().ConfigureAwait(false);
        }
    }
}
