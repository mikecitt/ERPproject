using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Data.Model;
using ERP.Domain.Dtos;

namespace ERP.Data.Extensions
{
    public static class OrderExtensions
    {
        public static IQueryable<OrderDto> ProjectOrderToOrderDto(this IQueryable<ShopOrder> query)
        {
            return query.Select(order => new OrderDto
            {
                Id = order.Id,
                BuyerId = order.BuyerId,
                OrderDate = order.OrderDate,
                DeliveryFee = order.DeliveryFee,
                OrderStatus = order.OrderStatus.ToString(),
                Subtotal = order.GetTotal(),
                UserName = order.User.Username,
                FullName = order.ShippingAddress_FullName,
                Address1 = order.ShippingAddress_Address1,
                Address2 = order.ShippingAddress_Address2,
                City = order.ShippingAddress_City,
                State = order.ShippingAddress_State,
                Zip = order.ShippingAddress_Zip,
                Country = order.ShippingAddress_Country,
                OrderItems = order.OrderItems.Select(item => new OrderItemDtoShow
                {
                    ProductId = item.Product.Id,
                    Name = item.Product.ProductName,
                    ImagePath = item.Product.ImagePath,
                    Price = (long)item.Product.Price,
                    Quantity = item.Quantity
                }).ToList()
            }).AsNoTracking();
        }
    }
}
