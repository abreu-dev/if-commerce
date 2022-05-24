using IfCommerce.Core.Queries;
using System.Collections.Generic;

namespace IfCommerce.Catalog.Application.Queries.Parameters
{
    public class CategoryParameters : QueryParameters
    {
        public IEnumerable<string> Name { get; set; }

        public CategoryParameters()
        {
            Order = "Name";
            Name = new List<string>();
        }
    }
}
