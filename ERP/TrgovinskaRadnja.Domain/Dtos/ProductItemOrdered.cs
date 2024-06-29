﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrgovinskaRadnja.Domain.Dtos
{
    [Owned]
    public class ProductItemOrdered
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
    }
}
