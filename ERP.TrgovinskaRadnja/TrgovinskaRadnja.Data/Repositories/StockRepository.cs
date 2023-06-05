using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrgovinskaRadnja.Data.Model;
using TrgovinskaRadnja.Domain.Core.Repositories;

namespace TrgovinskaRadnja.Data.Repositories
{
    public class StockRepository : BaseRepository<Stock>, IStockRepository
    {
        public StockRepository(TrgovinskaRadnjaDataBaseContext applicationContext, IMapper mapper) : base(applicationContext, mapper)
        {
        }
    }
}
