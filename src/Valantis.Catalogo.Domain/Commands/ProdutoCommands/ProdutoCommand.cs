using System;
using Valantis.Catalogo.Domain.Messaging;

namespace Valantis.Catalogo.Domain.Commands.ProdutoCommands
{
    public abstract class ProdutoCommand : Command
    {
        public Guid Id { get; protected set; }
        public string Nome { get; protected set; }
    }
}
