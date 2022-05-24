using AutoMapper;
using IfCommerce.Catalog.Application.Contracts.CategoryContracts;
using IfCommerce.Catalog.Application.Interfaces;
using IfCommerce.Catalog.Application.Queries.Parameters;
using IfCommerce.Catalog.Domain.Commands.CategoryCommands;
using IfCommerce.Catalog.Domain.Entities;
using IfCommerce.Catalog.Domain.Interfaces;
using IfCommerce.Core.Mediator;
using IfCommerce.Core.Queries;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace IfCommerce.Catalog.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IQueryService _queryService;

        public CategoryService(IMapper mapper,
                               ICategoryRepository categoryRepository,
                               IMediatorHandler mediatorHandler, 
                               IQueryService queryService)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _mediatorHandler = mediatorHandler;
            _queryService = queryService;
        }

        public PagedList<CategoryContract> GetPagedCategories(CategoryParameters parameters)
        {
            var source = _categoryRepository.Categories();

            source = _queryService.Ordering(source, parameters.Order);

            if (parameters.Name.Any())
            {
                source = _queryService.Filtering(source, "Name", parameters.Name);
            }

            var pagedList = _queryService.Pagination(source, parameters.Page, parameters.Size);
            return _mapper.Map<PagedList<CategoryContract>>(pagedList);
        }

        public CategoryContract GetCategoryById(Guid id)
        {
            return _mapper.Map<CategoryContract>(_categoryRepository.GetCategoryById(id));
        }

        public async Task AddCategory(AddCategoryContract contract)
        {
            var command = new AddCategoryCommand()
            {
                Category = _mapper.Map<Category>(contract)
            };

            await _mediatorHandler.SendCommand(command);
        }

        public async Task UpdateCategory(Guid id, UpdateCategoryContract contract)
        {
            var command = new UpdateCategoryCommand(id)
            {
                Category = _mapper.Map<Category>(contract)
            };

            await _mediatorHandler.SendCommand(command);
        }

        public async Task DeleteCategory(Guid id)
        {
            var command = new DeleteCategoryCommand(id);

            await _mediatorHandler.SendCommand(command);
        }
    }
}
