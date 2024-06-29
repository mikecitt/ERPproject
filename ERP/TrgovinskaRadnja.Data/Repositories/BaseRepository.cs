using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrgovinskaRadnja.Data.Extensions;
using TrgovinskaRadnja.Data.Model;
using TrgovinskaRadnja.Domain.Core.Repositories;
using TrgovinskaRadnja.Domain.Dtos;

namespace TrgovinskaRadnja.Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository where TEntity : Entity
    {
        protected IMapper Mapper { get; }
        protected TrgovinskaRadnjaDataBaseContext TrgovinskaRadnjaContext { get; }
        protected DbSet<TEntity> Items { get; }

        public BaseRepository(TrgovinskaRadnjaDataBaseContext applicationContext, IMapper mapper)
        {
            TrgovinskaRadnjaContext = applicationContext;
            Mapper = mapper;
            Items = TrgovinskaRadnjaContext.Set<TEntity>();
        }

        public async Task<TDto> CreateAsync<TDto>(TDto tDto)
        {
            var entity = Mapper.Map<TEntity>(tDto);

            await Items.AddAsync(entity);
            await TrgovinskaRadnjaContext.SaveChangesAsync();

            return Mapper.Map<TDto>(entity);
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            var entity = await Items
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

            if (entity != null)
            {
                Items.Remove(entity);
                await TrgovinskaRadnjaContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

       

        public async Task<TDto> GetByIdAsync<TDto>(int id)
        => Mapper.Map<TDto>(await Items.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted));

        public async Task UpdateAsync<TDto>(TDto tDto)
        {
            TEntity entity = Mapper.Map<TEntity>(tDto);

            TrgovinskaRadnjaContext.Set<TEntity>().Update(entity);
            await TrgovinskaRadnjaContext.SaveChangesAsync();
        }
    }
}
