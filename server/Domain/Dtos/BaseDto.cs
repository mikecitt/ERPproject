using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Dtos
{
    public abstract class BaseDto
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
