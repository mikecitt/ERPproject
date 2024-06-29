using System;
using System.Collections.Generic;
using ERP.Domain.Dtos;
using ERP.Domain.Enums;

namespace ERP.Data.Model;

public partial class ShopOrder : Entity
{

    public int UserId { get; set; }

    public string BuyerId { get; set; }

    public DateTime OrderDate { get; set; }

    public DateTime DeliveredDate { get; set; }

    public decimal Total { get; set; }

    public int DeliveryFee { get; set; }

    public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;


    public virtual ICollection<OrderItem> OrderItems { get; } = new List<OrderItem>();

    public virtual SiteUser User { get; set; }

    public string ShippingAddress_FullName { get; set; }
    public string ShippingAddress_Address1 { get; set; }
    public string ShippingAddress_Address2 { get; set; }
    public string ShippingAddress_City { get; set; }
    public string ShippingAddress_State { get; set; }
    public string ShippingAddress_Zip { get; set; }
    public string ShippingAddress_Country { get; set; }

    public string PaymentIntentId { get; set; }

    public decimal GetTotal() 
    {
        return Total + DeliveryFee;
    }
}
