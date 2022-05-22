using IfCommerce.Catalog.Domain.Commands.ProductCommands;
using IfCommerce.Core.Domain;

namespace IfCommerce.Catalog.Domain.Validators.ProductValidators
{
    public class DeleteProductCommandValidator : CommandValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            ValidateId();
        }
    }
}
