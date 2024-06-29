using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string BuyerId { get; set; }

        public string UserName { get; set; }
        public Address ShippingAddress { get; set; }
        public string FullName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItemDtoShow> OrderItems { get; set; }
        public decimal Subtotal { get; set; }
        public int DeliveryFee { get; set; }
        public string OrderStatus { get; set; }
    }
}
