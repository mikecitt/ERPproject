using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using ERP.Data.Model;
using ERP.Domain.Core.Services;
using ERP.Domain.Dtos;

namespace ERP.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly AppDataBaseContext _context;

        public UserController(IUserService userService, IMapper mapper, AppDataBaseContext context)
        {
            _userService = userService;
            _mapper = mapper;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] SiteUserDto userDto)
        {
            await _userService.CreateAsync(userDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {

            var user = await _context.SiteUsers.FindAsync(id);

            if (user == null) return NotFound();

           
            user.IsDeleted = true;
            var result = await _context.SaveChangesAsync() > 0;

            if (result) return Ok();

            return BadRequest(new ProblemDetails { Title = "Problem deleting user" });

        }

        [HttpGet("profile")]
        public async Task<ActionResult<SiteUser>> GetProfile()
        {
            var id = Convert.ToInt32(this.User.FindFirst("Id").Value);

            var profile = await _context.SiteUsers.SingleOrDefaultAsync(x => x.Id == id);

            return profile;
        }

        [HttpDelete("profile")]
        public async Task<ActionResult> DeleteProfile()
        {
            var id = Convert.ToInt32(this.User.FindFirst("Id").Value);

            var user = await _context.SiteUsers.FindAsync(id);

            if (user == null) return NotFound();


            user.IsDeleted = true;
            var result = await _context.SaveChangesAsync() > 0;

            if (result) return Ok();

            return BadRequest(new ProblemDetails { Title = "Problem deleting user" });
        }


        [HttpGet]
        public async Task<ActionResult<List<SiteUser>>> GetAll()
        {

            var profile = await _context.SiteUsers.Where(x => !x.IsDeleted).ToListAsync();

            return profile;
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
