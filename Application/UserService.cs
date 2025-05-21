using Domain.Entitys;
using Domain.Events;
using Domain.Models;
using Domain.Ports;
using Domain.Services;

namespace Application
{
    public class UserService : IUserService
    {
        private readonly IUserPort _userAdapter;
        private readonly IEventBusPort _eventBusAdapter;

        public UserService(IUserPort userAdapter, IEventBusPort eventBusAdapter)
        {
            _userAdapter = userAdapter;
            _eventBusAdapter = eventBusAdapter;
        }

        public async Task<User> AddAsync(CreateUser createUser)
        {
            var now = DateTime.UtcNow;
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = createUser.Name,
                CreatedAt = now,
                Active = true
            };

            _ = await _userAdapter.AddAsync(user).ConfigureAwait(false);

            var @event = new UserCreatedEvent
            {
                Id = user.Id,
                Name = user.Name,
                CreatedAt = user.CreatedAt,
                Active = user.Active
            };

            await _eventBusAdapter.PublishAsync(@event).ConfigureAwait(false);

            return user;
        }

        public async Task<User?> DeleteAsync(Guid id)
        {
            var user = await GetAsync(id).ConfigureAwait(false);

            if (user is null)
            {
                return null;
            }

            user.Active = false;
            await _userAdapter.UpdateAsync(user).ConfigureAwait(false);
            
            var @event = new UserDeletedEvent 
            { 
                Id = user.Id 
            }
            ;
            await _eventBusAdapter.PublishAsync(@event).ConfigureAwait(false);
            return user;
        }

        public async Task<User?> GetAsync(Guid id)
        {
            var user = await _userAdapter.GetAsync(id).ConfigureAwait(false);

            if (user is null || !user.Active)
            {
                return null;
            }

            return user;
        }

        public async Task UpdateAsync(User user)
        {
            await _userAdapter.UpdateAsync(user).ConfigureAwait(false);
            var @event = new UserUpdatedEvent
            {
                Id = user.Id,
                Name = user.Name,
                CreatedAt = user.CreatedAt,
                Active = user.Active
            };

            await _eventBusAdapter.PublishAsync(@event).ConfigureAwait(false);
        }
    }
}
