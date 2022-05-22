using IfCommerce.Catalog.Domain.Validators.ProductValidators;
using IfCommerce.Core.Messaging;
using System;

namespace IfCommerce.Catalog.Domain.Commands.ProductCommands
{
    public class DeleteProductCommand : Command
    {
        public DeleteProductCommand(Guid aggregateId) : base(aggregateId) { }

        public override bool IsValid()
        {
            ValidationResult = new DeleteProductCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
