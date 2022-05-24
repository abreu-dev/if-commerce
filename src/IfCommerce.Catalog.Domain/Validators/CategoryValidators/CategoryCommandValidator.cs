using FluentValidation;
using IfCommerce.Catalog.Domain.Commands.CategoryCommands;
using IfCommerce.Core.Domain;

namespace IfCommerce.Catalog.Domain.Validators.CategoryValidators
{
    public abstract class CategoryCommandValidator : CommandValidator<CategoryCommand>
    {
        public void ValidateDefault()
        {
            RuleFor(x => x.Category.Name)
                .NotEmpty()
                .WithErrorCode("MissingValue")
                .WithState(_ => "Name not informed")
                .WithMessage("The field 'Name' must be informed");
        }
    }
}
