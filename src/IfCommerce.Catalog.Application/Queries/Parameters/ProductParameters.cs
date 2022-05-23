using IfCommerce.Core.Queries;
using System.Collections.Generic;

namespace IfCommerce.Catalog.Application.Queries.Parameters
{
    public class ProductParameters : QueryParameters
    {
        public IEnumerable<string> Name { get; set; }

        public ProductParameters()
        {
            Order = "Name";
            Name = new List<string>();
        }
    }
}
