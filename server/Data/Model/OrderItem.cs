using System;
using System.Collections.Generic;

namespace ERP.Data.Model;

public partial class OrderItem : Entity
{

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal Cost { get; set; }


    public virtual ShopOrder Order { get; set; }

    public virtual Product Product { get; set; }
}
