using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrgovinskaRadnja.Domain.Dtos
{
    public class ProductDto : BaseDto
    {
        public string ProductName { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public bool? StockStatus { get; set; }

        public int CategoryId { get; set; }
    }
}
