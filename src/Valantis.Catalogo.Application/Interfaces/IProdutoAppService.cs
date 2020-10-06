using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Valantis.Catalogo.Application.ViewModels;

namespace Valantis.Catalogo.Application.Interfaces
{
    public interface IProdutoAppService : IDisposable
    {
        IEnumerable<ProdutoViewModel> ObterTodos();
        ProdutoViewModel ObterPorId(Guid id);

        Task Adicionar(ProdutoViewModel produtoViewModel);
    }
}
