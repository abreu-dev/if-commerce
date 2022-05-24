using IfCommerce.Catalog.Domain.Validators.CategoryValidators;
using IfCommerce.Core.Messaging;
using System;

namespace IfCommerce.Catalog.Domain.Commands.CategoryCommands
{
    public class DeleteCategoryCommand : Command
    {
        public DeleteCategoryCommand(Guid aggregateId) : base(aggregateId) { }

        public override bool IsValid()
        {
            ValidationResult = new DeleteCategoryCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
