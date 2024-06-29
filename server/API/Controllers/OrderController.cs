using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ERP.Data.Model;
using ERP.Domain.Dtos;
using ERP.Data.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ERP.API.Controllers
{
    [Authorize]
    [Route("api/order")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDataBaseContext _context;
        public OrdersController(AppDataBaseContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderDto>>> GetOrders()
        {
            return await _context.ShopOrders
                .ProjectOrderToOrderDto()
                .Where(x => x.BuyerId == this.User.FindFirst("Id").Value)
                .ToListAsync();
        }

        [HttpGet("allOrders")]
        public async Task<ActionResult<List<OrderDto>>> GetAllOrders()
        {
            return await _context.ShopOrders
                .ProjectOrderToOrderDto()
                .ToListAsync();
        }

        [HttpGet("{id}", Name = "GetOrder")]
        public async Task<ActionResult<OrderDto>> GetOrder(int id)
        {
            return await _context.ShopOrders
                .ProjectOrderToOrderDto()
                .Where(x => x.BuyerId == this.User.FindFirst("Id").Value && x.Id == id)
                .FirstOrDefaultAsync();
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateOrder(CreateOrderDto orderDto)
        {
            var basket = await _context.Carts
                .RetrieveCartWithItems(this.User.FindFirst("Id").Value)
                .FirstOrDefaultAsync();
            if (basket == null) return BadRequest(new ProblemDetails { Title = "Could not locate basket" });

            var items = new List<OrderItem>();

            decimal sum = 0;

            foreach (var item in basket.CartItems)
            {
                var productItem = await _context.Products.FindAsync(item.ProductId);

                sum += productItem.Price * item.Quantity;

            }
            var deliveryFee = sum > 5000 ? 0 : 250;


            Data.Model.ShopOrder order = new Data.Model.ShopOrder
            {
                BuyerId = this.User.FindFirst("Id").Value,
                UserId = Convert.ToInt32(this.User.FindFirst("Id").Value),
                ShippingAddress_FullName = orderDto.ShippingAddress.FullName,
                ShippingAddress_Address1 = orderDto.ShippingAddress.Address1,
                ShippingAddress_Address2 = orderDto.ShippingAddress.Address2,
                ShippingAddress_City = orderDto.ShippingAddress.City,
                ShippingAddress_State = orderDto.ShippingAddress.State,
                ShippingAddress_Zip = orderDto.ShippingAddress.Zip,
                ShippingAddress_Country = orderDto.ShippingAddress.Country,
                OrderDate = DateTime.Now,
                DeliveredDate = DateTime.Now.AddDays(3),
                DeliveryFee = deliveryFee,
                Total = sum,
                OrderStatus = Domain.Enums.OrderStatus.Pending,
                IsDeleted = false,
                PaymentIntentId = basket.PaymentIntentId
            };

            _context.ShopOrders.Add(order);
             _context.SaveChanges();


            foreach (var item in basket.CartItems)
            {
                var productItem = await _context.Products.FindAsync(item.ProductId);


                var ordItem = new Data.Model.OrderItem
                {
                    ProductId = productItem.Id,
                    Quantity = item.Quantity,
                    Cost = productItem.Price,
                    OrderId = order.Id
                };

               _context.OrderItems.Add(ordItem);
                
            }
           
            _context.Carts.Remove(basket);

          
            var result = await _context.SaveChangesAsync() > 0;
            if (result) return CreatedAtRoute("GetOrder", new { id = order.Id }, order.Id);

            return BadRequest("Problem creating order");
        }
    }
}
