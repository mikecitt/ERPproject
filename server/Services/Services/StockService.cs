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
    public class StockService : BaseService<StockDto>, IStockService
    {
        private readonly IStockRepository _stockRepository;

        public StockService(IStockRepository stockRepository) : base(stockRepository)
        {
            _stockRepository = stockRepository;
        }
    }
}
