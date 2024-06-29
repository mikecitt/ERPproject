using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Data.Model;
using ERP.Domain.Core.Repositories;
using ERP.Domain.Dtos;

namespace ERP.Data.Repositories
{
    public class ShopOrderRepository : BaseRepository<ShopOrder>, IShopOrderRepository
    {
        public ShopOrderRepository(AppDataBaseContext applicationContext, IMapper mapper) : base(applicationContext, mapper)
        {
        }

        public async Task<List<ShopOrderDto>> GetOrdersForUserIdAsync(int userId)
        {
            List<ShopOrder> orders = await Items.Where(x => x.UserId == userId).ToListAsync();

            List<ShopOrderDto> ordersDto = new List<ShopOrderDto>();

            foreach (var order in orders) 
            {
                ordersDto.Add(Mapper.Map<ShopOrderDto>(order));
            }

            return ordersDto;
        }
    }
}
