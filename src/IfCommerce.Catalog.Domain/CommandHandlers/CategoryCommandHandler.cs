using IfCommerce.Catalog.Domain.Commands.CategoryCommands;
using IfCommerce.Catalog.Domain.Interfaces;
using IfCommerce.Core.Mediator;
using IfCommerce.Core.Messaging;
using IfCommerce.Core.Messaging.Notifications;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IfCommerce.Catalog.Domain.CommandHandlers
{
    public class CategoryCommandHandler : CommandHandler,
        IRequestHandler<AddCategoryCommand, Unit>,
        IRequestHandler<UpdateCategoryCommand, Unit>,
        IRequestHandler<DeleteCategoryCommand, Unit>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryCommandHandler(IMediatorHandler mediatorHandler, 
                                      ICategoryRepository categoryRepository)
            : base(mediatorHandler)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Unit> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await _mediatorHandler.PublishValidationErrors(request);
                return Unit.Value;
            }

            if (_categoryRepository.Categories().Where(category => category.Name == request.Category.Name).Any())
            {
                await _mediatorHandler.PublishDomainNotification(
                    new DomainNotification("DuplicatedValue", "Name duplicated", "The field 'Name' must be unique"));
                return Unit.Value;
            }

            _categoryRepository.AddCategory(request.Category);
            await _categoryRepository.UnitOfWork.Commit();

            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await _mediatorHandler.PublishValidationErrors(request);
                return Unit.Value;
            }

            var category = _categoryRepository.GetCategoryById(request.AggregateId);
            if (category == null)
            {
                await _mediatorHandler.PublishDomainNotification(
                    new DomainNotification("NotFound", "Category not found", "The informed 'Category' was not found"));
                return Unit.Value;
            }

            if (_categoryRepository.Categories().Where(p => p.Id != request.AggregateId && p.Name == request.Category.Name).Any())
            {
                await _mediatorHandler.PublishDomainNotification(
                    new DomainNotification("DuplicatedValue", "Name duplicated", "The field 'Name' must be unique"));
                return Unit.Value;
            }

            category.Name = request.Category.Name;

            _categoryRepository.UpdateCategory(category);
            await _categoryRepository.UnitOfWork.Commit();

            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await _mediatorHandler.PublishValidationErrors(request);
                return Unit.Value;
            }

            var category = _categoryRepository.GetCategoryById(request.AggregateId);
            if (category == null)
            {
                return Unit.Value;
            }

            _categoryRepository.DeleteCategory(category);
            await _categoryRepository.UnitOfWork.Commit();

            return Unit.Value;
        }
    }
}
