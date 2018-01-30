using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetMastery.InventoryManager.Models
{
    public class PageListViewModel<T> : List<T>
    {
        public int TotalPages { get; set; }

        public int CurrentPage { get; set; }

        private PageListViewModel(List<T> items, int total, int current)
        {
            AddRange(items);
            TotalPages = total;
            CurrentPage = current;
        }

        public static PageListViewModel<T> CreatePage(IEnumerable<T> items, int current, int pageSize)
        {
            var totalCount = items.Count();
            var pagesCount = (int)Math.Ceiling(totalCount / (double)pageSize);
            var list = items.Skip(pageSize * (current - 1)).Take(pageSize).ToList();
            return new PageListViewModel<T>(list, pagesCount, current);
        }
    }
}