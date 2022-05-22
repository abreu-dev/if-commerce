namespace IfCommerce.Catalog.Domain.Validators.ProductValidators
{
    public class UpdateProductCommandValidator : ProductCommandValidator
    {
        public UpdateProductCommandValidator()
        {
            ValidateId();
            ValidateDefault();
        }
    }
}
