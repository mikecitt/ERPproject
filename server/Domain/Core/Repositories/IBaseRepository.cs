using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Core.Repositories
{
    public interface IBaseRepository
    {
        Task<TDto> GetByIdAsync<TDto>(int id);
        Task<TDto> CreateAsync<TDto>(TDto tDto);
        Task<bool> DeleteAsync(int id);
        Task UpdateAsync<TDto>(TDto tDto);
    }
}
