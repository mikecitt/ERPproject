using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrgovinskaRadnja.Data.Model;
using TrgovinskaRadnja.Domain.Core.Repositories;
using TrgovinskaRadnja.Domain.Dtos;

namespace TrgovinskaRadnja.Data.Repositories
{
    public class ShopOrderRepository : BaseRepository<ShopOrder>, IShopOrderRepository
    {
        public ShopOrderRepository(TrgovinskaRadnjaDataBaseContext applicationContext, IMapper mapper) : base(applicationContext, mapper)
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
