using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Core.Services
{
    public interface IBaseService<TDto>
    {
        Task<TDto> GetByIdAsync(int id);
        Task CreateAsync(TDto tDto);
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, TDto tDto);
    }
}
