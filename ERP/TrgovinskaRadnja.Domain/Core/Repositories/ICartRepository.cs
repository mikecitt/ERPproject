using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrgovinskaRadnja.Domain.Dtos;

namespace TrgovinskaRadnja.Domain.Core.Repositories
{
    public interface ICartRepository : IBaseRepository
    {
        public Task<CartDto> GetCartWithCartItemsAsync(int id);
    }
}
