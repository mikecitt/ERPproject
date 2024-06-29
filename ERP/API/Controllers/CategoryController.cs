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
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly AppDataBaseContext _context;


        public CategoryController(ICategoryService categoryService, IMapper mapper, AppDataBaseContext context)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _context = context;
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

        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetAll() 
        {
            var data = await _context.Categories.ToListAsync();
            return data;
        }


    }
}
