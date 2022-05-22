using System.Collections.Generic;

namespace IfCommerce.Catalog.Application.Responses
{
    public class PagedList<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }

        public PagedList(IEnumerable<T> items, int currentPage, int totalItems, int totalPages)
        {
            Data = items;
            CurrentPage = currentPage;
            TotalItems = totalItems;
            TotalPages = totalPages;
        }
    }
}
