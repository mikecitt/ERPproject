using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TrgovinskaRadnja.Data.Model;
using TrgovinskaRadnja.Domain.Core.Repositories;
using TrgovinskaRadnja.Domain.Dtos;

namespace TrgovinskaRadnja.Data.Repositories
{
    public class UserRepository : BaseRepository<SiteUser>, IUserRepository
    {
        public UserRepository(TrgovinskaRadnjaDataBaseContext applicationContext, IMapper mapper) : base(applicationContext, mapper)
        {
        }

        public async Task<SiteUserDto> GetUserWithEmailAsync(string email)
        {
            SiteUser user = await Items.Where(x => x.Email == email && !x.IsDeleted).FirstOrDefaultAsync();

            return Mapper.Map<SiteUserDto>(user);
        }

    }
}
