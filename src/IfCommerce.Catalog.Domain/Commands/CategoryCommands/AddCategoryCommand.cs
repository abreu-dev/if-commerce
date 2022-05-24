using IfCommerce.Catalog.Domain.Validators.CategoryValidators;
using System;

namespace IfCommerce.Catalog.Domain.Commands.CategoryCommands
{
    public class AddCategoryCommand : CategoryCommand
    {
        public AddCategoryCommand() : base(Guid.Empty) { }

        public override bool IsValid()
        {
            ValidationResult = new AddCategoryCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
