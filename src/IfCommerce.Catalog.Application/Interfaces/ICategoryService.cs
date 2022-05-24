using IfCommerce.Catalog.Application.Contracts.CategoryContracts;
using IfCommerce.Catalog.Application.Queries.Parameters;
using IfCommerce.Core.Queries;
using System;
using System.Threading.Tasks;

namespace IfCommerce.Catalog.Application.Interfaces
{
    public interface ICategoryService
    {
        PagedList<CategoryContract> GetPagedCategories(CategoryParameters parameters);

        CategoryContract GetCategoryById(Guid id);

        Task AddCategory(AddCategoryContract contract);
        Task UpdateCategory(Guid id, UpdateCategoryContract contract);
        Task DeleteCategory(Guid id);
    }
}
