using AutoMapper;
using IfCommerce.Catalog.Application.Contracts.ProductContracts;
using IfCommerce.Catalog.Domain.Entities;
using IfCommerce.Core.Query;

namespace IfCommerce.Catalog.Application.AutoMapper
{
    public class DomainToContractMappingProfile : Profile
    {
        public DomainToContractMappingProfile()
        {
            CreateMap<Product, ProductContract>();
            CreateMap<PagedList<Product>, PagedList<ProductContract>>();
        }
    }
}
