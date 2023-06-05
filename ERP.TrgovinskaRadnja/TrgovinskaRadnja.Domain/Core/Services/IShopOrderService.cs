using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrgovinskaRadnja.Domain.Dtos;

namespace TrgovinskaRadnja.Domain.Core.Services
{
    public interface IShopOrderService :  IBaseService<ShopOrderDto>
    {
        public Task<List<ShopOrderDto>> GetOrdersForUserIdAsync(int userId);
    }
}
