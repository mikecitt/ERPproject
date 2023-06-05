using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TrgovinskaRadnja.Domain.Core.Services;
using TrgovinskaRadnja.Domain.Dtos;

namespace TrgovinskaRadnja.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] SiteUserDto userDto)
        {
            await _userService.CreateAsync(userDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {

            try
            {
                await _userService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                SiteUserDto userDto = await _userService.GetByIdAsync(id);
                return Ok(userDto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] SiteUserDto userDto)
        {
            await _userService.UpdateAsync(id, userDto);

            return NoContent();
        }

    }
}
