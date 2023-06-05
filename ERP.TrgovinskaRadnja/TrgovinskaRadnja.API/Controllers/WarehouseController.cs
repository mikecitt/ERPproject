using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TrgovinskaRadnja.Domain.Core.Services;
using TrgovinskaRadnja.Domain.Dtos;

namespace TrgovinskaRadnja.API.Controllers
{
    [Route("api/warehouse")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;
        private readonly IMapper _mapper;

        public WarehouseController(IWarehouseService warehouseService, IMapper mapper)
        {
            _warehouseService = warehouseService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] WarehouseDto warehouseDto)
        {
            await _warehouseService.CreateAsync(warehouseDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {

            try
            {
                await _warehouseService.DeleteAsync(id);
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
                WarehouseDto warehouseDto = await _warehouseService.GetByIdAsync(id);
                return Ok(warehouseDto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] WarehouseDto warehouseDto)
        {
            await _warehouseService.UpdateAsync(id, warehouseDto);

            return NoContent();
        }

    }
}
