using IfCommerce.Catalog.Domain.Entities;
using IfCommerce.Core.Messaging;
using System;

namespace IfCommerce.Catalog.Domain.Commands.ProductCommands
{
    public abstract class ProductCommand : Command
    {
        public Product Product { get; set; }

        protected ProductCommand(Guid aggregateId) : base(aggregateId) { }
    }
}
