using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;

namespace IfCommerce.Core.Queries
{
    public class QueryService : IQueryService
    {
        private const string CHARACTER_CONTAINS = "*";

        public IQueryable<T> Ordering<T>(IQueryable<T> source, string order)
        {
            return source.OrderBy(order);
        }

        public IQueryable<T> Filtering<T>(IQueryable<T> source, string propertyName, IEnumerable<string> propertyValues)
        {
            var predicate = new StringBuilder();
            var parameters = new List<string>();

            foreach (var property in propertyValues.Select((value, index) => new { value, index }))
            {
                string statement;

                if (property.value.StartsWith(CHARACTER_CONTAINS) && property.value.EndsWith(CHARACTER_CONTAINS))
                {
                    statement = "{0}.ToLower().Contains(@{1})";
                    parameters.Add(property.value[1..^1].ToLower());
                }
                else if (property.value.StartsWith(CHARACTER_CONTAINS))
                {
                    statement = "{0}.ToLower().EndsWith(@{1})";
                    parameters.Add(property.value[1..].ToLower());
                }
                else if (property.value.EndsWith(CHARACTER_CONTAINS))
                {
                    statement = "{0}.ToLower().StartsWith(@{1})";
                    parameters.Add(property.value[0..^1].ToLower());
                }
                else
                {
                    statement = "{0}.Equals(@{1})";
                    parameters.Add(property.value);
                }

                if (predicate.Length > 0)
                {
                    var orStatement = string.Format(" OR {0}", statement);
                    predicate.Append(string.Format(orStatement, propertyName, property.index));
                }
                else
                {
                    predicate.Append(string.Format(statement, propertyName, property.index));
                }
            }

            return source.Where(predicate.ToString(), parameters.ToArray());
        }

        public PagedList<T> Pagination<T>(IQueryable<T> source, int page, int size)
        {
            var count = source.Count();
            var items = source.Skip((page - 1) * size).Take(size).ToList();
            return new PagedList<T>(items, count, page, size);
        }
    }
}
