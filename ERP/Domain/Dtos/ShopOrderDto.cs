using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Dtos
{
    public class ShopOrderDto : BaseDto
    {
        public int UserId { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime DeliveredDate { get; set; }

        public decimal Total { get; set; }
    }
}
