using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Domain.Dtos;

namespace ERP.Domain.Core.Services
{
    public interface IUserService : IBaseService<SiteUserDto>
    {
        public Task<SiteUserDto> GetUserWithEmailAsync(string email);
        public Task RegisterAsync(SiteUserDto userDto);

        public Task<string> GetUserRoleAsString(int userId);
    }
}
