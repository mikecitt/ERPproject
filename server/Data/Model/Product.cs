using System;
using System.Collections.Generic;

namespace ERP.Data.Model;

public partial class Product : Entity
{

    public string ProductName { get; set; }

    public string ImagePath { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public bool? StockStatus { get; set; }

    public int CategoryId { get; set; }


    public virtual CartItem CartItem { get; set; }

    public virtual Category Category { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; } = new List<OrderItem>();

    public int QuantityInStock { get; set; }

}
