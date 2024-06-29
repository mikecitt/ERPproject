using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Dtos
{
    public class StockDto : BaseDto
    {
        public int StockQuantity { get; set; }
        public int WarehouseId { get; set; }
    }
}
