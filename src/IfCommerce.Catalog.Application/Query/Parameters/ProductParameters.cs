using IfCommerce.Core.Query;
using System.Collections.Generic;

namespace IfCommerce.Catalog.Application.Query.Parameters
{
    public class ProductParameters : QueryParameters
    {
        public IEnumerable<string> Name { get; set; }

        public ProductParameters()
        {
            Name = new List<string>();
        }
    }
}
