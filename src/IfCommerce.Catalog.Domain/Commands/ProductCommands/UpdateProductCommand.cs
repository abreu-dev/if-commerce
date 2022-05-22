using IfCommerce.Catalog.Domain.Validators.ProductValidators;
using System;

namespace IfCommerce.Catalog.Domain.Commands.ProductCommands
{
    public class UpdateProductCommand : ProductCommand
    {
        public UpdateProductCommand(Guid aggregateId) : base(aggregateId) { }

        public override bool IsValid()
        {
            ValidationResult = new UpdateProductCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
