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
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(TrgovinskaRadnjaDataBaseContext applicationContext, IMapper mapper) : base(applicationContext, mapper)
        {
        }
    }
}
