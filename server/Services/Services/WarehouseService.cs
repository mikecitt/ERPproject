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
    public class WarehouseService : BaseService<WarehouseDto>, IWarehouseService
    {
        private readonly IWarehouseRepository _warehouseRepository;
        public WarehouseService(IWarehouseRepository warehouseRepository) : base(warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }
    }
}
