using Domain.Entitys;
using Domain.Models;
using Domain.Ports;
using Domain.Services;

namespace Application
{
    public class RentalService : IRentalService
    {
        private readonly IRentalPort _rentalAdapter;

        public RentalService(IRentalPort rentalAdapter)
        {
            _rentalAdapter = rentalAdapter;
        }

        public async Task<Rental> AddAsync(CreateRental createRental)
        {
            var existsMovie = await _rentalAdapter.ExistsMovieAsync(createRental.MovieId).ConfigureAwait(false);
            if (!existsMovie)
            {
                throw new Exception("The movie was not found");
            }

            var user = await _rentalAdapter.GetUserAsync(createRental.UserId).ConfigureAwait(false);
            if (user is null || !user.Active)
            {
                throw new Exception("The user was not found");
            }

            var rental = new Rental
            {
                Id = Guid.NewGuid(),
                MovieId = createRental.MovieId,
                UserId = createRental.UserId,
                WithdrawDate = createRental.WithdrawDate,
                ReturnDate = createRental.ReturnDate,
                Amount = createRental.Amount
            };

            return await _rentalAdapter.AddAsync(rental).ConfigureAwait(false);
        }

        public Task<IEnumerable<Rental>> GetAllAsync()
        {
            return _rentalAdapter.GetAllAsync();
        }

        public Task<Rental?> GetByIdAsync(Guid id)
        {
            return _rentalAdapter.GetByIdAsync(id);
        }

        public Task UpdateAsync(Rental rental)
        {
            return _rentalAdapter.UpdateAsync(rental);
        }
    }
}
