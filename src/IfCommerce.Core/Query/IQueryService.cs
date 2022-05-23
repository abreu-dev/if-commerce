using System.Collections.Generic;
using System.Linq;

namespace IfCommerce.Core.Query
{
    public interface IQueryService
    {
        PagedList<T> Pagination<T>(IQueryable<T> source, int page, int size);

        IQueryable<T> Ordering<T>(IQueryable<T> source, string order);

        IQueryable<T> Filtering<T>(IQueryable<T> source, string propertyName, IEnumerable<string> propertyValues);
    }
}
