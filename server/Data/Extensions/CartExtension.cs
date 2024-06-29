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
    public static class CartExtension
    {
        public static CartDto MapCartToDto(this Cart cart)
        {
            return new CartDto
            {
                Id = cart.Id,
                BuyerId = cart.BuyerId,
                PaymentIntentId = cart.PaymentIntentId,
                ClientSecret = cart.ClientSecret,
                Items = cart.CartItems.Select(item => new CartItemDto
                {
                    Name = item.Product.ProductName,
                    ProductId = item.ProductId,
                    Price = item.Product.Price,
                    ImagePath = item.Product.ImagePath,
                    Quantity = item.Quantity
                }).ToList()
            };
        }

        public static IQueryable<Cart> RetrieveCartWithItems(this IQueryable<Cart> query, string buyerId)
        {
            return query
                .Include(i => i.CartItems)
                .ThenInclude(p => p.Product)
                .Where(basket => basket.BuyerId == buyerId);
        }
    }
}
