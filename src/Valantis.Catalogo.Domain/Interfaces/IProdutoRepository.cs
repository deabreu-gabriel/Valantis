using System;
using System.Collections.Generic;
using Valantis.Catalogo.Domain.Entities;

namespace Valantis.Catalogo.Domain.Interfaces
{
    public interface IProdutoRepository: IDisposable
    {
        IEnumerable<Produto> ObterTodos();
        Produto ObterPorId(Guid id);

        void Adicionar(Produto produto);
    }
}
