using System;
using System.Collections.Generic;

namespace IfCommerce.Core.Query
{
    public class PagedList<T>
    {
        public IEnumerable<T> Data { get; set; }

        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int TotalCount { get; private set; }
        public int PageSize { get; private set; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public PagedList() { }

        public PagedList(List<T> items, int count, int page, int size)
        {
            Data = items;
            CurrentPage = page;
            TotalPages = (int)Math.Ceiling(count / (double)size);
            TotalCount = count;
            PageSize = size;
        }
    }
}
