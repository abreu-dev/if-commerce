using IfCommerce.Catalog.Application.Interfaces;
using IfCommerce.Catalog.Application.Services;
using IfCommerce.Catalog.Domain.CommandHandlers;
using IfCommerce.Catalog.Domain.Commands.CategoryCommands;
using IfCommerce.Catalog.Domain.Commands.ProductCommands;
using IfCommerce.Catalog.Domain.Interfaces;
using IfCommerce.Catalog.Infra.Data.Context;
using IfCommerce.Catalog.Infra.Data.Repositories;
using IfCommerce.Core.Mediator;
using IfCommerce.Core.Messaging.Notifications;
using IfCommerce.Core.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace IfCommerce.Catalog.Infra.CrossCutting.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            // Application - Services
            services.AddScoped<IHealthService, HealthService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();

            // Application - Query
            services.AddScoped<IQueryService, QueryService>();

            // Domain - Mediator
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Domain - Notification
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<AddProductCommand, Unit>, ProductCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateProductCommand, Unit>, ProductCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteProductCommand, Unit>, ProductCommandHandler>();

            services.AddScoped<IRequestHandler<AddCategoryCommand, Unit>, CategoryCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateCategoryCommand, Unit>, CategoryCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteCategoryCommand, Unit>, CategoryCommandHandler>();

            // Infra Data - Contexts
            services.AddScoped<ICatalogContext, CatalogContext>();

            // Infra Data - Repositories
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
        }
    }
}
