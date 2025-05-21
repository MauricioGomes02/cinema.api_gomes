using Domain.Entitys;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Usuario.Api.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [ProducesResponseType<Rental>(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAsync([FromBody] CreateUser createUser)
        {
            var user = await _userService.AddAsync(createUser);
            return CreatedAtRoute(new { id = user.Id }, user);
        }

        [HttpGet("{id}")]
        [ProducesResponseType<Rental>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var user = await _userService.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPut]
        [ProducesResponseType<Rental>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync([FromBody] User user)
        {
            await _userService.UpdateAsync(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType<Rental>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var user = await _userService.DeleteAsync(id);
            if (user is null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
