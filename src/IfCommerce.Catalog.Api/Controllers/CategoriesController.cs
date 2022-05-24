using IfCommerce.Catalog.Application.Contracts.CategoryContracts;
using IfCommerce.Catalog.Application.Interfaces;
using IfCommerce.Catalog.Application.Queries.Parameters;
using IfCommerce.Core.Messaging.Notifications;
using IfCommerce.Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IfCommerce.Catalog.Api.Controllers
{
    public class CategoriesController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(INotificationHandler<DomainNotification> notifications,
                                    ICategoryService categoryService)
            : base(notifications)
        {
            _categoryService = categoryService;
        }

        [HttpGet("[controller]:paginated")]
        public PagedList<CategoryContract> GetPagedCategories([FromQuery] CategoryParameters parameters)
        {
            return _categoryService.GetPagedCategories(parameters);
        }

        [HttpGet("[controller]/{id:guid}")]
        public CategoryContract Get(Guid id)
        {
            return _categoryService.GetCategoryById(id);
        }

        [HttpPost("[controller]")]
        public async Task<IActionResult> Post([FromBody] AddCategoryContract contract)
        {
            await _categoryService.AddCategory(contract);
            return CustomResponse();
        }

        [HttpPut("[controller]/{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateCategoryContract contract)
        {
            await _categoryService.UpdateCategory(id, contract);
            return CustomResponse();
        }

        [HttpDelete("[controller]/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _categoryService.DeleteCategory(id);
            return CustomResponse();
        }
    }
}
