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
    public class CartRepository : BaseRepository<Cart>, ICartRepository
    {
        public CartRepository(TrgovinskaRadnjaDataBaseContext applicationContext, IMapper mapper) : base(applicationContext, mapper)
        {
        }

        public async Task<CartDto> GetCartWithCartItemsAsync(int id)
        {
            Cart cart = await Items.Where(x=>x.Id == id).Include(x=>x.CartItems).FirstOrDefaultAsync();

            return Mapper.Map<CartDto>(cart);
        }
    }
}
