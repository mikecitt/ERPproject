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
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(TrgovinskaRadnjaDataBaseContext applicationContext, IMapper mapper) : base(applicationContext, mapper)
        {
        }
    }
}
