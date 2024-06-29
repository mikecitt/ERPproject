using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Dtos
{
    public class UserDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public CartDto Cart{ get; set; }
    }
}
