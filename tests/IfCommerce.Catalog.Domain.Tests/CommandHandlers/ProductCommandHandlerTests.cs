using IfCommerce.Catalog.Domain.CommandHandlers;
using IfCommerce.Catalog.Domain.Commands.ProductCommands;
using IfCommerce.Catalog.Domain.Entities;
using IfCommerce.Catalog.Domain.Interfaces;
using IfCommerce.Core.Data;
using IfCommerce.Core.Mediator;
using IfCommerce.Core.Messaging;
using IfCommerce.Core.Messaging.Notifications;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;

namespace IfCommerce.Catalog.Domain.Tests.CommandHandlers
{
    public class ProductCommandHandlerTests
    {
        private readonly Mock<IMediatorHandler> _mediatorHandler;
        private readonly Mock<IProductRepository> _productRepository;
        private readonly ProductCommandHandler _productCommandHandler;

        public ProductCommandHandlerTests()
        {
            _mediatorHandler = new Mock<IMediatorHandler>();
            _productRepository = new Mock<IProductRepository>();
            _productRepository.Setup(mock => mock.UnitOfWork).Returns(new Mock<IUnitOfWork>().Object);
            _productCommandHandler = new ProductCommandHandler(_mediatorHandler.Object, _productRepository.Object);
        }

        #region AddProductCommand
        [Fact]
        public void Handle_AddProductCommand_ShouldPublishValidationErrors_WhenInvalidCommand()
        {
            // Arrange
            var command = new AddProductCommand()
            {
                Product = new Product()
                {
                    Name = string.Empty
                }
            };

            // Act
            _productCommandHandler.Handle(command, CancellationToken.None).Wait();

            // Assert
            _mediatorHandler.Verify(mock => mock.PublishValidationErrors(command), Times.Once);
            _productRepository.Verify(mock => mock.AddProduct(It.IsAny<Product>()), Times.Never);
            _productRepository.Verify(mock => mock.UnitOfWork.Commit(), Times.Never);
        }

        [Fact]
        public void Handle_AddProductCommand_ShouldPublishDomainNotification_WhenDuplicatedName()
        {
            // Arrange
            var command = new AddProductCommand()
            {
                Product = new Product()
                {
                    Name = "Name"
                }
            };

            _productRepository.Setup(mock => mock.Products())
                .Returns(new List<Product>() { new Product() { Name = command.Product.Name } }.AsQueryable());

            // Act
            _productCommandHandler.Handle(command, CancellationToken.None).Wait();

            // Assert
            _mediatorHandler.Verify(mock => mock.PublishDomainNotification(It.Is<DomainNotification>(x => x.Type == "DuplicatedValue" &&
                x.Message == "Name duplicated" && x.Detail == "The field 'Name' must be unique")), Times.Once);
            _productRepository.Verify(mock => mock.AddProduct(It.IsAny<Product>()), Times.Never);
            _productRepository.Verify(mock => mock.UnitOfWork.Commit(), Times.Never);
        }

        [Fact]
        public void Handle_AddProductCommand_ShouldAddAndCommit_WhenValid()
        {
            // Arrange
            var command = new AddProductCommand()
            {
                Product = new Product()
                {
                    Name = "Name"
                }
            };

            _productRepository.Setup(mock => mock.Products())
                .Returns(new List<Product>() { new Product() { Name = "Other Name" } }.AsQueryable());

            // Act
            _productCommandHandler.Handle(command, CancellationToken.None).Wait();

            // Assert
            _mediatorHandler.Verify(mock => mock.PublishValidationErrors(It.IsAny<Command>()), Times.Never);
            _productRepository.Verify(mock => mock.AddProduct(command.Product), Times.Once);
            _productRepository.Verify(mock => mock.UnitOfWork.Commit(), Times.Once);
        }
        #endregion

        #region UpdateProductCommand
        [Fact]
        public void Handle_UpdateProductCommand_ShouldPublishValidationErrors_WhenInvalidCommand()
        {
            // Arrange
            var command = new UpdateProductCommand(Guid.Empty)
            {
                Product = new Product()
                {
                    Name = string.Empty
                }
            };

            // Act
            _productCommandHandler.Handle(command, CancellationToken.None).Wait();

            // Assert
            _mediatorHandler.Verify(mock => mock.PublishValidationErrors(command), Times.Once);
            _productRepository.Verify(mock => mock.UpdateProduct(It.IsAny<Product>()), Times.Never);
            _productRepository.Verify(mock => mock.UnitOfWork.Commit(), Times.Never);
        }

        [Fact]
        public void Handle_UpdateProductCommand_ShouldPublishDomainNotification_WhenProductNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            var command = new UpdateProductCommand(id)
            {
                Product = new Product()
                {
                    Id = id,
                    Name = "Name"
                }
            };

            _productRepository.Setup(mock => mock.GetProductById(command.AggregateId)).Returns((Product)null);

            // Act
            _productCommandHandler.Handle(command, CancellationToken.None).Wait();

            // Assert
            _mediatorHandler.Verify(mock => mock.PublishDomainNotification(It.Is<DomainNotification>(x => x.Type == "NotFound" &&
                x.Message == "Product not found" && x.Detail == "The informed 'Product' was not found")), Times.Once);
            _productRepository.Verify(mock => mock.UpdateProduct(It.IsAny<Product>()), Times.Never);
            _productRepository.Verify(mock => mock.UnitOfWork.Commit(), Times.Never);
        }

        [Fact]
        public void Handle_UpdateProductCommand_ShouldPublishDomainNotification_WhenDuplicatedName()
        {
            // Arrange
            var id = Guid.NewGuid();
            var command = new UpdateProductCommand(id)
            {
                Product = new Product()
                {
                    Id = id,
                    Name = "Name"
                }
            };

            _productRepository.Setup(mock => mock.GetProductById(command.AggregateId)).Returns(command.Product);

            _productRepository.Setup(mock => mock.Products())
                .Returns(new List<Product>() { new Product() { Id = Guid.NewGuid(), Name = command.Product.Name } }.AsQueryable());

            // Act
            _productCommandHandler.Handle(command, CancellationToken.None).Wait();

            // Assert
            _mediatorHandler.Verify(mock => mock.PublishDomainNotification(It.Is<DomainNotification>(x => x.Type == "DuplicatedValue" &&
                x.Message == "Name duplicated" && x.Detail == "The field 'Name' must be unique")), Times.Once);
            _productRepository.Verify(mock => mock.UpdateProduct(It.IsAny<Product>()), Times.Never);
            _productRepository.Verify(mock => mock.UnitOfWork.Commit(), Times.Never);
        }

        [Fact]
        public void Handle_UpdateProductCommand_ShouldUpdateAndCommit_WhenValid()
        {
            // Arrange
            var id = Guid.NewGuid();
            var command = new UpdateProductCommand(id)
            {
                Product = new Product()
                {
                    Id = id,
                    Name = "Updated Name"
                }
            };
            var dbEntity = new Product() { Id = id, Name = "Name" };

            _productRepository.Setup(mock => mock.GetProductById(command.AggregateId)).Returns(dbEntity);

            _productRepository.Setup(mock => mock.Products())
                .Returns(new List<Product>() { new Product() { Id = Guid.NewGuid(), Name = "Other Name" }, dbEntity }.AsQueryable());

            // Act
            _productCommandHandler.Handle(command, CancellationToken.None).Wait();

            // Assert
            _mediatorHandler.Verify(mock => mock.PublishValidationErrors(It.IsAny<Command>()), Times.Never);
            _productRepository.Verify(mock => mock.UpdateProduct(dbEntity), Times.Once);
            _productRepository.Verify(mock => mock.UpdateProduct(It.Is<Product>(x => x.Name == command.Product.Name)), Times.Once);
            _productRepository.Verify(mock => mock.UnitOfWork.Commit(), Times.Once);
        }
        #endregion

        #region DeleteProductCommand
        [Fact]
        public void Handle_DeleteProductCommand_ShouldPublishValidationErrors_WhenInvalidCommand()
        {
            // Arrange
            var command = new DeleteProductCommand(Guid.Empty);

            // Act
            _productCommandHandler.Handle(command, CancellationToken.None).Wait();

            // Assert
            _mediatorHandler.Verify(mock => mock.PublishValidationErrors(command), Times.Once);
            _productRepository.Verify(mock => mock.DeleteProduct(It.IsAny<Product>()), Times.Never);
            _productRepository.Verify(mock => mock.UnitOfWork.Commit(), Times.Never);
        }

        [Fact]
        public void Handle_DeleteProductCommand_ShouldDoNothing_WhenProductNotFound()
        {
            // Arrange
            var command = new DeleteProductCommand(Guid.NewGuid());

            _productRepository.Setup(mock => mock.GetProductById(command.AggregateId)).Returns((Product)null);

            // Act
            _productCommandHandler.Handle(command, CancellationToken.None).Wait();

            // Assert
            _mediatorHandler.Verify(mock => mock.PublishValidationErrors(It.IsAny<Command>()), Times.Never);
            _productRepository.Verify(mock => mock.DeleteProduct(It.IsAny<Product>()), Times.Never);
            _productRepository.Verify(mock => mock.UnitOfWork.Commit(), Times.Never);
        }

        [Fact]
        public void Handle_DeleteProductCommand_ShouldDeleteAndCommit_WhenValid()
        {
            // Arrange
            var command = new DeleteProductCommand(Guid.NewGuid());

            var product = new Product() { Id = command.AggregateId };
            _productRepository.Setup(mock => mock.GetProductById(command.AggregateId)).Returns(product);

            // Act
            _productCommandHandler.Handle(command, CancellationToken.None).Wait();

            // Assert
            _mediatorHandler.Verify(mock => mock.PublishValidationErrors(It.IsAny<Command>()), Times.Never);
            _productRepository.Verify(mock => mock.DeleteProduct(product), Times.Once);
            _productRepository.Verify(mock => mock.UnitOfWork.Commit(), Times.Once);
        }
        #endregion
    }
}
