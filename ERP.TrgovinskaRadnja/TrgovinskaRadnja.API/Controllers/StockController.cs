using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TrgovinskaRadnja.Domain.Core.Services;
using TrgovinskaRadnja.Domain.Dtos;

namespace TrgovinskaRadnja.API.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;
        private readonly IMapper _mapper;

        public StockController(IStockService stockService, IMapper mapper)
        {
            _stockService = stockService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] StockDto stockDto)
        {
            await _stockService.CreateAsync(stockDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {

            try
            {
                await _stockService.DeleteAsync(id);
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
                StockDto stockDto = await _stockService.GetByIdAsync(id);
                return Ok(stockDto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] StockDto stockDto)
        {
            await _stockService.UpdateAsync(id, stockDto);

            return NoContent();
        }

    }
}
