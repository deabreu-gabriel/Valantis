using System;
using System.Threading;
using MediatR;
using System.Threading.Tasks;
using Valantis.Catalogo.Domain.Commands.ProdutoCommands;
using Valantis.Catalogo.Domain.Entities;
using Valantis.Catalogo.Domain.Interfaces;

namespace Valantis.Catalogo.Domain.CommandHandlers
{
    public class ProdutoCommandHandler :
        IRequestHandler<AdicionarProdutoCommand, bool>
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoCommandHandler(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public Task<bool> Handle(AdicionarProdutoCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                return Task.FromResult(request.ValidationResult.IsValid);
            }

            var produto = new Produto(Guid.NewGuid(), request.Nome);
            _produtoRepository.Adicionar(produto);

            return Task.FromResult(true);
        }
    }
}