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
    public class CartItemService : BaseService<CartItemDto>, ICartItemService
    {
        private readonly ICartItemRepository _cartItemRepository;
        public CartItemService(ICartItemRepository cartItemRepository) : base(cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }
    }
}
