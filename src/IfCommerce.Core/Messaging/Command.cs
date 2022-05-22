using FluentValidation.Results;
using MediatR;
using System;

namespace IfCommerce.Core.Messaging
{
    public abstract class Command : Message, IRequest<Unit>
    {
        public ValidationResult ValidationResult { get; set; }

        protected Command(Guid aggregateId)
        {
            ValidationResult = new ValidationResult();
            AggregateId = aggregateId;
        }

        public virtual bool IsValid()
        {
            return ValidationResult.IsValid;
        }
    }
}
