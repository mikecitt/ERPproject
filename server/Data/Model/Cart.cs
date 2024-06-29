using System;
using System.Collections.Generic;

namespace ERP.Data.Model;

public partial class Cart : Entity
{

    public string BuyerId{ get; set; }

    public virtual ICollection<CartItem> CartItems { get; } = new List<CartItem>();

    public string PaymentIntentId { get; set; }
    public string ClientSecret { get; set; }


    public void AddItem(Product product, int quantity)
    {
        if (CartItems.All(item => item.ProductId != product.Id))
        {
            CartItems.Add(new CartItem { Product = product, Quantity = quantity });
            return;
        }

        var existingItem = CartItems.FirstOrDefault(item => item.ProductId == product.Id);
        if (existingItem != null) existingItem.Quantity += quantity;
    }

    public void RemoveItem(int productId, int quantity = 1)
    {
        var item = CartItems.FirstOrDefault(basketItem => basketItem.ProductId == productId);
        if (item == null) return;
        item.Quantity -= quantity;
        if (item.Quantity == 0) CartItems.Remove(item);
    }
}
