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
    public class BaseService<TDto> : IBaseService<TDto> where TDto : BaseDto
    {
        protected readonly IBaseRepository _baseRepository;

        public BaseService(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task CreateAsync(TDto tDto)
        {
            await _baseRepository.CreateAsync<TDto>(tDto);
        }

        public async Task DeleteAsync(int id)
        {
            bool deleted = await _baseRepository.DeleteAsync(id);
            if (!deleted)
            {
                throw new KeyNotFoundException();
            }
        }

      

        public async Task<TDto> GetByIdAsync(int id)
        {
            TDto tDto = await _baseRepository.GetByIdAsync<TDto>(id);
            if (tDto == null)
            {
                throw new KeyNotFoundException();
            }

            return tDto;
        }

        public async Task UpdateAsync(int id, TDto tDto)
        {
            tDto.Id = id;
            await _baseRepository.UpdateAsync<TDto>(tDto);
        }
    }
}
