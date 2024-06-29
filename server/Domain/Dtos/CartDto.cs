using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Dtos
{
    public class CartDto : BaseDto
    {
        public string BuyerId { get; set; }
        public List<CartItemDto> Items { get; set; }

        public string PaymentIntentId { get; set; }


        public string ClientSecret { get; set; }
    }
}
