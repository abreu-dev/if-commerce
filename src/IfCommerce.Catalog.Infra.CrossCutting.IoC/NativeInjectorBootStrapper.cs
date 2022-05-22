using IfCommerce.Catalog.Application.Interfaces;
using IfCommerce.Catalog.Application.Services;
using IfCommerce.Catalog.Domain.CommandHandlers;
using IfCommerce.Catalog.Domain.Commands.ProductCommands;
using IfCommerce.Catalog.Domain.Interfaces;
using IfCommerce.Catalog.Infra.Data.Context;
using IfCommerce.Catalog.Infra.Data.Repositories;
using IfCommerce.Core.Mediator;
using IfCommerce.Core.Messaging.Notifications;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace IfCommerce.Catalog.Infra.CrossCutting.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Application - Services
            services.AddScoped<IHealthService, HealthService>();
            services.AddScoped<IProductService, ProductService>();

            // Domain - Mediator
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Domain - Notification
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<AddProductCommand, Unit>, ProductCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateProductCommand, Unit>, ProductCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteProductCommand, Unit>, ProductCommandHandler>();

            // Infra Data - Contexts
            services.AddScoped<CatalogContext>();

            // Infra Data - Repositories
            services.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}
