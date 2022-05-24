namespace IfCommerce.Catalog.Domain.Validators.CategoryValidators
{
    public class UpdateCategoryCommandValidator : CategoryCommandValidator
    {
        public UpdateCategoryCommandValidator()
        {
            ValidateId();
            ValidateDefault();
        }
    }
}
