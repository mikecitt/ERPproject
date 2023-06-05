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
    public class OrderItemService : BaseService<OrderItemDto>, IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        public OrderItemService(IOrderItemRepository orderItemRepository) : base(orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }
    }
}
