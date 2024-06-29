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
    public class StockService : BaseService<StockDto>, IStockService
    {
        private readonly IStockRepository _stockRepository;

        public StockService(IStockRepository stockRepository) : base(stockRepository)
        {
            _stockRepository = stockRepository;
        }
    }
}
