﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrgovinskaRadnja.Domain.Dtos;

namespace TrgovinskaRadnja.Domain.Core.Services
{
    public interface ICartService : IBaseService<CartDto>
    {
        public Task<CartDto> GetCartWithCartItemsAsync(int id);

    }
}
