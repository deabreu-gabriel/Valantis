using System;
using System.Collections.Generic;
using System.Linq;
using Valantis.Catalogo.Domain.Entities;
using Valantis.Catalogo.Domain.Interfaces;

namespace Valantis.Catalogo.Infra.Data.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly IList<Produto> _produtos;

        public ProdutoRepository()
        {
            _produtos = new List<Produto>()
            {
                new Produto(Guid.Parse("f489492d-dfea-4003-bb6a-f37088b6456c"), "Produto 1"),
                new Produto(Guid.Parse("c755cafd-de71-4273-a020-4a8a7c1db9e6"), "Produto 2"),
                new Produto(Guid.Parse("381fab8a-d13b-4d31-b201-b0e3bd8d5bdf"), "Produto 3")
            };
        }

        public IEnumerable<Produto> ObterTodos()
        {
            return _produtos;
        }

        public Produto ObterPorId(Guid id)
        {
            return _produtos.FirstOrDefault(p => p.Id == id);
        }

        public void Adicionar(Produto produto)
        {
            _produtos.Add(produto);
        }

        public void Dispose()
        {
        }
    }
}
