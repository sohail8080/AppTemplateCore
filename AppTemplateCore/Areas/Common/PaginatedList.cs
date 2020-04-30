using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Areas.Common
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }


        public PaginatedList(List<T> items, int totalPages, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = totalPages;

            this.AddRange(items);
        }


        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }


        public static async Task<PaginatedList<T>> CreateAsync(
                        IQueryable<T> source, 
                        int pageIndex, 
                        int pageSize)
        {
            var count = await source.CountAsync();

            var items = await source.Skip(
                (pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();


            //The CreateAsync method is used to create the PaginatedList<T>.
            //A constructor can't create the PaginatedList<T> object; 
            //constructors can't run asynchronous code.
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }



        public static PaginatedList<T> CreateAsync(
                List<T> source,
                int totalPages,
                int pageIndex,
                int pageSize)
        {
            var count = source.Count();

            return new PaginatedList<T>(source, totalPages, count, pageIndex, pageSize);
        }




    }
}
