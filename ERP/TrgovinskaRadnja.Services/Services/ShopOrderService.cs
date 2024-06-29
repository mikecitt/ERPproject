using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrgovinskaRadnja.Domain.Core.Repositories;
using TrgovinskaRadnja.Domain.Core.Services;
using TrgovinskaRadnja.Domain.Dtos;

namespace TrgovinskaRadnja.Services.Services
{
    public class ShopOrderService : BaseService<ShopOrderDto>, IShopOrderService
    {
        private readonly IShopOrderRepository _shopOrderRepository;
        public ShopOrderService(IShopOrderRepository shopOrderRepository) : base(shopOrderRepository)
        {
            _shopOrderRepository = shopOrderRepository;
        }

        public async Task<List<ShopOrderDto>> GetOrdersForUserIdAsync(int userId)
        {
           return await _shopOrderRepository.GetOrdersForUserIdAsync(userId);
        }
    }
}
