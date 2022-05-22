using FluentValidation;
using IfCommerce.Catalog.Domain.Commands.ProductCommands;
using IfCommerce.Core.Domain;

namespace IfCommerce.Catalog.Domain.Validators.ProductValidators
{
    public abstract class ProductCommandValidator : CommandValidator<ProductCommand>
    {
        public void ValidateDefault()
        {
            RuleFor(x => x.Product.Name)
                .NotEmpty()
                .WithErrorCode("MissingValue")
                .WithState(_ => "Name not informed")
                .WithMessage("The field 'Name' must be informed");
        }
    }
}
