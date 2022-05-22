using FluentValidation.Results;
using IfCommerce.Core.Domain;
using MediatR;
using System;

namespace IfCommerce.Core.Messaging
{
    public abstract class Command : Message, IRequest<ValidationResult>
    {
        public ValidationResult ValidationResult { get; set; }
        public Entity Entity { get; set; }

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
