using IfCommerce.Catalog.Domain.Commands.ProductCommands;
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
    public class ProductCommandHandler : CommandHandler,
        IRequestHandler<AddProductCommand, Unit>,
        IRequestHandler<UpdateProductCommand, Unit>,
        IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly IProductRepository _productRepository;

        public ProductCommandHandler(IMediatorHandler mediatorHandler, 
                                     IProductRepository productRepository)
            : base(mediatorHandler)
        {
            _productRepository = productRepository;
        }

        public async Task<Unit> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await _mediatorHandler.PublishValidationErrors(request);
                return Unit.Value;
            }

            if (_productRepository.Products().Where(product => product.Name == request.Product.Name).Any())
            {
                await _mediatorHandler.PublishDomainNotification(
                    new DomainNotification("DuplicatedValue", "Name duplicated", "The field 'Name' must be unique"));
                return Unit.Value;
            }

            _productRepository.AddProduct(request.Product);
            await _productRepository.UnitOfWork.Commit();

            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await _mediatorHandler.PublishValidationErrors(request);
                return Unit.Value;
            }

            var product = _productRepository.GetProductById(request.AggregateId);
            if (product == null)
            {
                await _mediatorHandler.PublishDomainNotification(
                    new DomainNotification("NotFound", "Product not found", "The informed 'Product' was not found"));
                return Unit.Value;
            }

            if (_productRepository.Products().Where(p => p.Id != request.AggregateId && p.Name == request.Product.Name).Any())
            {
                await _mediatorHandler.PublishDomainNotification(
                    new DomainNotification("DuplicatedValue", "Name duplicated", "The field 'Name' must be unique"));
                return Unit.Value;
            }

            product.Name = request.Product.Name;

            _productRepository.UpdateProduct(product);
            await _productRepository.UnitOfWork.Commit();

            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await _mediatorHandler.PublishValidationErrors(request);
                return Unit.Value;
            }

            var product = _productRepository.GetProductById(request.AggregateId);
            if (product == null)
            {
                return Unit.Value;
            }

            _productRepository.DeleteProduct(product);
            await _productRepository.UnitOfWork.Commit();

            return Unit.Value;
        }
    }
}
