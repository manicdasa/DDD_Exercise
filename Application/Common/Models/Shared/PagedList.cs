using System.Collections.Generic;
using System.Linq;

namespace GhostWriter.Application.Common.Models.Shared
{
    public class PagedList<T>
    {
        /// <summary>
        /// Gets or sets the total count.
        /// </summary>
        public int TotalCount { get; set; }

        public List<T> Items { get; set; }

        public PagedList()
        {
            Items = new List<T>();
        }

        public PagedList(IEnumerable<T> items, int totalCount)
            :this()
        {
            TotalCount = totalCount;
            Items.AddRange(items);
        }

        public PagedList(IQueryable<T> source, int page = 0, int take = int.MaxValue)
            :this()
        {
            TotalCount = source.Count();
            Items.AddRange(source.Skip(page * take).Take(take).ToList());
        }

        public PagedList(IEnumerable<T> source, int skip = 0, int take = int.MaxValue)
            : this()
        {
            TotalCount = source.Count();
            Items.AddRange(source.Skip(skip*take).Take(take).ToList());
        }
    }
}
