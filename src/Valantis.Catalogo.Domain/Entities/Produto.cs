using System;

namespace Valantis.Catalogo.Domain.Entities
{
    public class Produto
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }

        public Produto(Guid id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
}
