using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Dtos
{
    public class CartItemDto : BaseDto
    {
        public int ProductId { get; set; }
        public decimal Price { get; set; }

        public string Name { get; set; }
        public string ImagePath { get; set; }
        public int Quantity { get; set; }
    }
}
