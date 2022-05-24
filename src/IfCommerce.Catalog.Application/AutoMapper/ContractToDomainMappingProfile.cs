using AutoMapper;
using IfCommerce.Catalog.Application.Contracts.CategoryContracts;
using IfCommerce.Catalog.Application.Contracts.ProductContracts;
using IfCommerce.Catalog.Domain.Entities;

namespace IfCommerce.Catalog.Application.AutoMapper
{
    public class ContractToDomainMappingProfile : Profile
    {
        public ContractToDomainMappingProfile()
        {
            CreateMap<AddProductContract, Product>();
            CreateMap<UpdateProductContract, Product>();

            CreateMap<AddCategoryContract, Category>();
            CreateMap<UpdateCategoryContract, Category>();
        }
    }
}
