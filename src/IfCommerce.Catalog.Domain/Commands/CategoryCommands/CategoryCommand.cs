using IfCommerce.Catalog.Domain.Entities;
using IfCommerce.Core.Messaging;
using System;

namespace IfCommerce.Catalog.Domain.Commands.CategoryCommands
{
    public abstract class CategoryCommand : Command
    {
        public Category Category { get; set; }

        protected CategoryCommand(Guid aggregateId) : base(aggregateId) { }
    }
}
