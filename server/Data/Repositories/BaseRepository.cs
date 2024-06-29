using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Data.Extensions;
using ERP.Data.Model;
using ERP.Domain.Core.Repositories;
using ERP.Domain.Dtos;

namespace ERP.Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository where TEntity : Entity
    {
        protected IMapper Mapper { get; }
        protected AppDataBaseContext appContext { get; }
        protected DbSet<TEntity> Items { get; }

        public BaseRepository(AppDataBaseContext applicationContext, IMapper mapper)
        {
            appContext = applicationContext;
            Mapper = mapper;
            Items = appContext.Set<TEntity>();
        }

        public async Task<TDto> CreateAsync<TDto>(TDto tDto)
        {
            var entity = Mapper.Map<TEntity>(tDto);

            await Items.AddAsync(entity);
            await appContext.SaveChangesAsync();

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
                await appContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

       

        public async Task<TDto> GetByIdAsync<TDto>(int id)
        => Mapper.Map<TDto>(await Items.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted));

        public async Task UpdateAsync<TDto>(TDto tDto)
        {
            TEntity entity = Mapper.Map<TEntity>(tDto);

            appContext.Set<TEntity>().Update(entity);
            await appContext.SaveChangesAsync();
        }
    }
}
