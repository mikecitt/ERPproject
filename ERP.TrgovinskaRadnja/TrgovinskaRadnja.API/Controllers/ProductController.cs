using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrgovinskaRadnja.Data.Extensions;
using TrgovinskaRadnja.Data.Model;
using TrgovinskaRadnja.Domain.Core.Services;
using TrgovinskaRadnja.Domain.Dtos;

namespace TrgovinskaRadnja.API.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class Product : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly TrgovinskaRadnjaDataBaseContext _context;


        public Product(IProductService productService, IMapper mapper, TrgovinskaRadnjaDataBaseContext context)
        {
            _productService = productService;
            _mapper = mapper;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ProductDto productDto)
        {
            await _productService.CreateAsync(productDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {

            try
            {
                await _productService.DeleteAsync(id);
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
                ProductDto productDto = await _productService.GetByIdAsync(id);
                return Ok(productDto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Data.Model.Product>>> GetProducts([FromQuery] ProductParams productParams)
        {
            var query = _context.Products
                .Sort(productParams.OrderBy)
                .Search(productParams.SearchTerm)
                .AsQueryable();

            var products = await PaginatedList<Data.Model.Product>.ToPagedList<Data.Model.Product>(query, productParams.PageNumber,
                productParams.PageSize);

            Response.AddPaginationHeader(products.MetaData);

            return products;

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] ProductDto productDto)
        {
            await _productService.UpdateAsync(id, productDto);

            return NoContent();
        }

    }
}
