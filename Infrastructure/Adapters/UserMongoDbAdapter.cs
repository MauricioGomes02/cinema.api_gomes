using Domain.Entitys;
using Domain.Ports;
using Infrastructure.Configurations.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Adapters
{
    public class UserMongoDbAdapter : IUserPort
    {
        private readonly IMongoCollection<User> _collection;

        public UserMongoDbAdapter(IOptions<MongoDbAdapterConfigurationModel> configuration, IMongoClient client)
        {
            var database = client.GetDatabase(configuration.Value.DatabaseName);
            _collection = database.GetCollection<User>(configuration.Value.CollectionName);
        }

        public async Task<User> AddAsync(User user)
        {
            await _collection.InsertOneAsync(user).ConfigureAwait(false);
            return user;
        }

        public async Task<User?> GetAsync(Guid id)
        {
            var cursor = await _collection.FindAsync(x => x.Id ==  id);
            return cursor.FirstOrDefault();
        }

        public async Task UpdateAsync(User user)
        {
            var updateDefinition = new UpdateDefinitionBuilder<User>()
                .Set(x => x.Name, user.Name)
                .Set(x => x.Active, user.Active);

            await _collection.UpdateOneAsync(x => x.Id == user.Id, updateDefinition);
        }
    }
}
