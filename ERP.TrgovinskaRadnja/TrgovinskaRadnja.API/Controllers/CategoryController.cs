using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TrgovinskaRadnja.Domain.Core.Services;
using TrgovinskaRadnja.Domain.Dtos;

namespace TrgovinskaRadnja.API.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CategoryDto categoryDto)
        {
            await _categoryService.CreateAsync(categoryDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {

            try
            {
                await _categoryService.DeleteAsync(id);
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
                CategoryDto categoryDto = await _categoryService.GetByIdAsync(id);
                return Ok(categoryDto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] CategoryDto categoryDto)
        {
            await _categoryService.UpdateAsync(id, categoryDto);

            return NoContent();
        }

    }
}
