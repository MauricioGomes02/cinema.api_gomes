using Domain.Entitys;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Aluguel.Api.Controllers
{
    [ApiController]
    [Route("rentals")]
    public class RentalController : ControllerBase
    {
        private readonly IRentalService _rentalService;

        public RentalController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpPost]
        [ProducesResponseType<Rental>(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAsync([FromBody] CreateRental createRental)
        {
            var rental = await _rentalService.AddAsync(createRental);
            return CreatedAtRoute(new { id = rental.Id }, rental);
        }

        [HttpGet]
        [ProducesResponseType<Rental>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            var rentals = await _rentalService.GetAllAsync();
            return Ok(rentals);
        }

        [HttpGet("{id}")]
        [ProducesResponseType<Rental>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var rental = await _rentalService.GetByIdAsync(id);

            if (rental is null)
            {
                return NotFound();
            }

            return Ok(rental);
        }

        [HttpPut]
        [ProducesResponseType<Rental>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync([FromBody] Rental rental)
        {
            await _rentalService.UpdateAsync(rental);
            return NoContent();
        }
    }
}
