using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using ERP.Data.Model;
using ERP.Domain.Core.Services;
using ERP.Domain.Dtos;
using ERP.Data.Extensions;


namespace ERP.API.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;
        private readonly AppDataBaseContext _context;
        

        public CartController(ICartService cartService, IMapper mapper, AppDataBaseContext context)
        {
            _cartService = cartService;
            _mapper = mapper;
            _context = context;

        }

        [HttpDelete]
        public async Task<ActionResult> RemoveCartItem(int productId, int quantity = 1)
        {
            var cart = await RetrieveCart(GetBuyerId());

            if (cart == null) return NotFound();

            cart.RemoveItem(productId, quantity);

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return Ok();

            return BadRequest(new ProblemDetails { Title = "Problem removing item from the basket" });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                CartDto cartDto = await _cartService.GetByIdAsync(id);
                return Ok(cartDto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] CartDto cartDto)
        {
    
            await _cartService.UpdateAsync(id, cartDto);

            return NoContent();
        }

        [HttpGet(Name = "GetCart")]
        public async Task<ActionResult<CartDto>> GetBasket()
        {
            var cart = await RetrieveCart(GetBuyerId());

            if (cart == null) return NotFound();

            return cart.MapCartToDto();
        }

        [HttpPost]
        public async Task<ActionResult<CartDto>> AddItemToCart(int productId, int quantity)
        {
            var basket = await RetrieveCart(GetBuyerId());
            if (basket == null) basket = CreateCart();

            var product = await _context.Products.FindAsync(productId);

            if (product == null) return BadRequest(new ProblemDetails { Title = "Product not found" });

            basket.AddItem(product, quantity);

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return CreatedAtRoute("GetCart", basket.MapCartToDto());

            return BadRequest(new ProblemDetails { Title = "Problem saving item to basket" });
        }



        private Cart CreateCart()
        {
            var buyerId = this.User.FindFirst("Id")?.Value;

            if (string.IsNullOrEmpty(buyerId))
            {
                buyerId = Guid.NewGuid().ToString();
                var cookieOptions = new CookieOptions { Secure=true, SameSite=SameSiteMode.None, IsEssential = true, Expires = DateTime.Now.AddDays(30) };
                Response.Cookies.Append("buyerId", buyerId, cookieOptions);
                
            }

            var cart = new Cart { BuyerId = buyerId};
            _context.Carts.Add(cart);
            return cart;
        }

        private string GetBuyerId()
        {
            return this.User.FindFirst("Id")?.Value ?? Request.Cookies["buyerId"];
        }

        private async Task<Cart> RetrieveCart(string buyerId)
        {
            if (string.IsNullOrEmpty(buyerId))
            {
                Response.Cookies.Delete("buyerId");
                return null;
            }


            return await _context.Carts.Include(x => x.CartItems).ThenInclude(c => c.Product)
                .FirstOrDefaultAsync(cart => cart.BuyerId == buyerId);
        }

    }
}
