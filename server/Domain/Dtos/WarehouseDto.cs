using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Dtos
{
    public class WarehouseDto : BaseDto
    {
        public int Capacity { get; set; }

        public string Location { get; set; }
    }
}
