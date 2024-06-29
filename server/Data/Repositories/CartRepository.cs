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
    public class CartRepository : BaseRepository<Cart>, ICartRepository
    {
        public CartRepository(AppDataBaseContext applicationContext, IMapper mapper) : base(applicationContext, mapper)
        {
        }

        public async Task<CartDto> GetCartWithCartItemsAsync(int id)
        {
            Cart cart = await Items.Where(x=>x.Id == id).Include(x=>x.CartItems).FirstOrDefaultAsync();

            return Mapper.Map<CartDto>(cart);
        }
    }
}
