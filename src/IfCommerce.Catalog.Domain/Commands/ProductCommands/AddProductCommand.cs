using IfCommerce.Catalog.Domain.Validators.ProductValidators;
using System;

namespace IfCommerce.Catalog.Domain.Commands.ProductCommands
{
    public class AddProductCommand : ProductCommand
    {
        public AddProductCommand() : base(Guid.Empty) { }

        public override bool IsValid()
        {
            ValidationResult = new AddProductCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
