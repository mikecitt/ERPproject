using System;
using System.Collections.Generic;

namespace TrgovinskaRadnja.Data.Model;

public partial class ShopOrder : Entity
{

    public int UserId { get; set; }

    public DateTime OrderDate { get; set; }

    public DateTime DeliveredDate { get; set; }

    public decimal Total { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; } = new List<OrderItem>();

    public virtual SiteUser User { get; set; }
}
