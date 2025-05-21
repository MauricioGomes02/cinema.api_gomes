using Domain.Entitys;
using Domain.Events;
using Domain.Ports;

namespace Aluguel.Worker
{
    public class Worker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public Worker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var adapter = scope.ServiceProvider.GetRequiredService<IEventBusPort>();
            await adapter.SubscribeAsync<UserCreatedEvent>(async x =>
            {
                var rentalAdapter = scope.ServiceProvider.GetRequiredService<IRentalPort>();
                var user = new User
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreatedAt = x.CreatedAt,
                    Active = x.Active,
                };

                await rentalAdapter.AddUserAsync(user).ConfigureAwait(false);
            }).ConfigureAwait(false);

            await adapter.SubscribeAsync<UserUpdatedEvent>(async x =>
            {
                var rentalAdapter = scope.ServiceProvider.GetRequiredService<IRentalPort>();
                var user = new User
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreatedAt = x.CreatedAt,
                    Active = x.Active,
                };

                await rentalAdapter.UpdateUserAsync(user).ConfigureAwait(false);
            }).ConfigureAwait(false);

            await adapter.SubscribeAsync<UserDeletedEvent>(async x =>
            {
                var rentalAdapter = scope.ServiceProvider.GetRequiredService<IRentalPort>();
                var user = await rentalAdapter.GetUserAsync(x.Id).ConfigureAwait(false);
                user.Active = false;

                await rentalAdapter.UpdateUserAsync(user).ConfigureAwait(false);
            }).ConfigureAwait(false);
            while (!stoppingToken.IsCancellationRequested)
            {
                
            }
        }
    }
}
