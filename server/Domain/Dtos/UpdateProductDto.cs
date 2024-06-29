using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Dtos
{
    public class UpdateProductDto : BaseDto
    {

        [Required]
        public string ProductName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public IFormFile File { get; set; }

        [Required]
        public int QuantityInStock { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
