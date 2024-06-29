using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Domain.Core.Repositories;
using ERP.Domain.Core.Services;
using ERP.Domain.Dtos;

namespace ERP.Services.Services
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
