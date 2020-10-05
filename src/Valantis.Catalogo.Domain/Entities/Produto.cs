using System;

namespace Valantis.Catalogo.Domain.Entities
{
    public class Produto
    {
        public Guid Id { get; }
        public string Nome { get; }

        public Produto(Guid id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
}
