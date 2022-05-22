using FluentValidation;
using IfCommerce.Core.Messaging;

namespace IfCommerce.Core.Domain
{
    public abstract class CommandValidator<T> : AbstractValidator<T> where T : Command 
    {
        protected void ValidateId()
        {
            RuleFor(x => x.AggregateId)
                .NotEmpty()
                .WithErrorCode("MissingValue")
                .WithState(_ => "Id not informed")
                .WithMessage("The field 'Id' must be informed");
        }
    }
}
