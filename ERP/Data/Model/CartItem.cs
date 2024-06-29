using System;
using System.Collections.Generic;

namespace ERP.Data.Model;

public partial class CartItem : Entity
{

    public int CartId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }


    public virtual Cart Cart { get; set; }

    public virtual Product Product { get; set; }
}
