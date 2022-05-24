using IfCommerce.Catalog.Domain.Commands.CategoryCommands;
using IfCommerce.Core.Domain;

namespace IfCommerce.Catalog.Domain.Validators.CategoryValidators
{
    public class DeleteCategoryCommandValidator : CommandValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidator()
        {
            ValidateId();
        }
    }
}
