using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Domain.Dtos;

namespace ERP.Data.Extensions
{
    public static class PaginatedListExtension
    {

        public static async Task<PaginatedList<T>> ToPagedList<T>(this PaginatedList<T> paginatedList, IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedList<T>(items, count, pageNumber, pageSize);

        }
    }
}
