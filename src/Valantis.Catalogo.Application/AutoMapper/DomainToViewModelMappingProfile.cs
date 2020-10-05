using AutoMapper;
using Valantis.Catalogo.Application.ViewModels;
using Valantis.Catalogo.Domain.Entities;

namespace Valantis.Catalogo.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Produto, ProdutoViewModel>();
        }
    }
}
