using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using ERP.Domain.Core.Services;
using ERP.Domain.Dtos;

namespace ERP.API.Controllers
{
    [Route("api/shopOrder")]
    [ApiController]
    public class ShopOrder : ControllerBase
    {
        private readonly IShopOrderService _shopOrderService;
        private readonly IMapper _mapper;

        public ShopOrder(IShopOrderService shopOrderService, IMapper mapper)
        {
            _shopOrderService = shopOrderService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ShopOrderDto shopOrderDto)
        {
            await _shopOrderService.CreateAsync(shopOrderDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {

            try
            {
                await _shopOrderService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("user{id}")]
        public async Task<IActionResult> GetOrdersByUserId(int userId) 
        {
            List<ShopOrderDto> orders =await _shopOrderService.GetOrdersForUserIdAsync(userId);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                ShopOrderDto shopOrderDto = await _shopOrderService.GetByIdAsync(id);
                return Ok(shopOrderDto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] ShopOrderDto shopOrderDto)
        {
            await _shopOrderService.UpdateAsync(id, shopOrderDto);

            return NoContent();
        }

    }
}
