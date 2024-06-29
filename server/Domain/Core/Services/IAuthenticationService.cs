using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Core.Services
{
    public interface IAuthenticationService
    {
        public Task<string> AuthenticateAccountAsync(string email, string password,bool passwordHashed);

    }
}
