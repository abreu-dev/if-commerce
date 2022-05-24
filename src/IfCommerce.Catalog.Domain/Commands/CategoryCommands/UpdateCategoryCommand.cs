using IfCommerce.Catalog.Domain.Validators.CategoryValidators;
using System;

namespace IfCommerce.Catalog.Domain.Commands.CategoryCommands
{
    public class UpdateCategoryCommand : CategoryCommand
    {
        public UpdateCategoryCommand(Guid aggregateId) : base(aggregateId) { }

        public override bool IsValid()
        {
            ValidationResult = new UpdateCategoryCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
