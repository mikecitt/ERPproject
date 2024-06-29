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
    public class CartService : BaseService<CartDto>, ICartService
    {
        private readonly ICartRepository _cartRepository;
        public CartService(ICartRepository cartRepository) : base(cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<CartDto> GetCartWithCartItemsAsync(int id)
        {
            return await _cartRepository.GetCartWithCartItemsAsync(id);
        }
    }
}
