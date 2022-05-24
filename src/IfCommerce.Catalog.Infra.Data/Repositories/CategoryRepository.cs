using IfCommerce.Catalog.Domain.Entities;
using IfCommerce.Catalog.Domain.Interfaces;
using IfCommerce.Catalog.Infra.Data.Context;
using IfCommerce.Core.Data;
using System;
using System.Linq;

namespace IfCommerce.Catalog.Infra.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ICatalogContext _catalogContext;

        public IUnitOfWork UnitOfWork => _catalogContext;

        public CategoryRepository(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }

        public IQueryable<Category> Categories()
        {
            return _catalogContext.Categories;
        }

        public Category GetCategoryById(Guid id)
        {
            return _catalogContext.Categories.SingleOrDefault(p => p.Id == id);
        }

        public void AddCategory(Category category)
        {
            _catalogContext.Categories.Add(category);
        }

        public void UpdateCategory(Category category)
        {
            _catalogContext.Categories.Update(category);
        }

        public void DeleteCategory(Category category)
        {
            _catalogContext.Categories.Remove(category);
        }
    }
}
