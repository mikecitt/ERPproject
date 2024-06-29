using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.Data.Model;
using ERP.Domain.Core.Services;
using ERP.Domain.Dtos;
using ERP.Domain.Dtos.Requests;
using ERP.Data.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace ERP.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;
        private readonly JwtSettings _jwtSettings;
        private readonly AppDataBaseContext _context;


        public AuthenticationController(IAuthenticationService authenticationService, IUserService userService, JwtSettings jwtSettings,AppDataBaseContext context)
        {
            _authenticationService = authenticationService;
            _jwtSettings = jwtSettings;
            _userService = userService;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> AuthenticateUserAsync(LoginRequest loginRequest)
        {
            try
            {
                string token = await _authenticationService.AuthenticateAccountAsync(loginRequest.Email, loginRequest.Password,true);
                SiteUserDto user = await _userService.GetUserWithEmailAsync(loginRequest.Email);
                Cart userBasket = await RetrieveBasket(user.Id.ToString());
                Cart anonBasket = await RetrieveBasket(Request.Cookies["buyerId"]);
                
                if (anonBasket != null)
                {
                    if (userBasket != null) _context.Carts.Remove(userBasket);
                    anonBasket.BuyerId = user.Id.ToString();
                    Response.Cookies.Delete("buyerId");
                    await _context.SaveChangesAsync();
                }
                
                return new UserDto
                {
                    Email = user.Email,
                    Token = token,
                    Cart = anonBasket != null ? anonBasket.MapCartToDto() : userBasket?.MapCartToDto()
                };

            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException)
            {
                return BadRequest("Invalid credentials");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] SiteUserDto userDto)
        {

            try
            {
                userDto.Role = Domain.Enums.UserType.Customer;
                await _userService.RegisterAsync(userDto);
                return StatusCode(201);
            }
            catch (KeyNotFoundException)
            {
                return BadRequest();
            }
        }



        [Authorize]
        [HttpGet("currentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userService.GetByIdAsync(Convert.ToInt32(this.User.FindFirst("Id").Value));

            var token = await _authenticationService.AuthenticateAccountAsync(user.Email, user.Password,true);

            var userBasket = await RetrieveBasket(user.Id.ToString());

            return new UserDto
            {
                Email = user.Email,
                Token = token,
                Cart = userBasket?.MapCartToDto()
            };
        }



        private async Task<Cart> RetrieveBasket(string buyerId)
        {
            if (string.IsNullOrEmpty(buyerId))
            {
                Response.Cookies.Delete("buyerId");
                return null;
            }

            return await _context.Carts
                .Include(i => i.CartItems)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(basket => basket.BuyerId == buyerId);
        }
    }
}
