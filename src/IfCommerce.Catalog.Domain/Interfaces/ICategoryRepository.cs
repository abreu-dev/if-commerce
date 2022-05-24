using IfCommerce.Catalog.Domain.Entities;
using IfCommerce.Core.Data;
using System;
using System.Linq;

namespace IfCommerce.Catalog.Domain.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IQueryable<Category> Categories();

        Category GetCategoryById(Guid id);

        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
    }
}
