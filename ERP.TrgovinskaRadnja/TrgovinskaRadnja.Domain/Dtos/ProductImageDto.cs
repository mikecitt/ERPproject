using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrgovinskaRadnja.Domain.Dtos
{
    public class ProductImageDto : BaseDto
    {
        public int ProductId { get; set; }

        public string ImagePath { get; set; }
    }
}
