using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.Data.Extensions;
using ERP.Data.Model;
using ERP.Domain.Core.Services;
using ERP.Domain.Dtos;

namespace ERP.API.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class Product : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly AppDataBaseContext _context;


        public Product(IProductService productService, IMapper mapper, AppDataBaseContext context)
        {
            _productService = productService;
            _mapper = mapper;
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Data.Model.Product>> CreateProduct([FromForm] CreateProductDto productDto)
        {
            Data.Model.Product product = _mapper.Map<Data.Model.Product>(productDto);


            product.StockStatus = product.QuantityInStock > 0;
            _context.Products.Add(product);

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return CreatedAtRoute("GetProduct", new { Id = product.Id }, product);

            return BadRequest(new ProblemDetails { Title = "Problem creating new product" });
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null) return NotFound();

            product.IsDeleted = true;

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return Ok();

            return BadRequest(new ProblemDetails { Title = "Problem deleting product" });
        }


        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDto>> GetByIdAsync(int id)
        {
            try
            {
                ProductDto productDto = await _productService.GetByIdAsync(id);
                return productDto;
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
                .Where(x=> !x.IsDeleted)
                .AsQueryable();

            var products = await PaginatedList<Data.Model.Product>.ToPagedList<Data.Model.Product>(query, productParams.PageNumber,
                productParams.PageSize);

            Response.AddPaginationHeader(products.MetaData);

            return products;

        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult<Data.Model.Product>> UpdateProduct([FromForm] UpdateProductDto productDto)
        {
            var product = await _context.Products.FindAsync(productDto.Id);

            if (product == null) return NotFound();

            _mapper.Map(productDto, product);

            if(productDto.File != null) 
            {
                product.ImagePath = "/images/products/" + productDto.File.FileName;
            }
            product.StockStatus = product.QuantityInStock > 0;

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return Ok(product);

            return BadRequest(new ProblemDetails { Title = "Problem updating product" });
        }

    }
}
