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
    public class WarehouseService : BaseService<WarehouseDto>, IWarehouseService
    {
        private readonly IWarehouseRepository _warehouseRepository;
        public WarehouseService(IWarehouseRepository warehouseRepository) : base(warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }
    }
}
