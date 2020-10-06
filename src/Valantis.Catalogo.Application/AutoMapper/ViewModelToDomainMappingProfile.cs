using AutoMapper;
using Valantis.Catalogo.Application.ViewModels;
using Valantis.Catalogo.Domain.Commands.ProdutoCommands;

namespace Valantis.Catalogo.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ProdutoViewModel, AdicionarProdutoCommand>()
                .ConstructUsing(c => new AdicionarProdutoCommand(c.Nome));
        }
    }
}
