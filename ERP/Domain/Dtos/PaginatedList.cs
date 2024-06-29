using Microsoft.EntityFrameworkCore;


namespace ERP.Domain.Dtos
{
    public class PaginatedList<T> : List<T>
    {

        public MetaData MetaData { get; set; }


        public PaginatedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            MetaData = new MetaData
            {
                TotalCount = count,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };
            AddRange(items);
        }

        public const int maxPageSize = 30;
     
        public static async Task<PaginatedList<T>> ToPagedList<T>(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedList<T>(items, count, pageNumber, pageSize);

        }
    }
}
