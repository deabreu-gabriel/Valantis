using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Valantis.Catalogo.Application.Interfaces;
using Valantis.Catalogo.Application.ViewModels;
using Valantis.Catalogo.Domain.Commands.ProdutoCommands;
using Valantis.Catalogo.Domain.Interfaces;
using Valantis.Catalogo.Domain.Mediator;
using Valantis.Catalogo.Domain.Messaging;

namespace Valantis.Catalogo.Application.Services
{
    public class ProdutoAppService : IProdutoAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoAppService(IMapper mapper,
                                 IMediatorHandler mediatorHandler,
                                 IProdutoRepository produtoRepository)
        {
            _mapper = mapper;
            _mediatorHandler = mediatorHandler;
            _produtoRepository = produtoRepository;
        }

        public IEnumerable<ProdutoViewModel> ObterTodos()
        {
            return _mapper.Map<IEnumerable<ProdutoViewModel>>(_produtoRepository.ObterTodos());
        }
        public ProdutoViewModel ObterPorId(Guid id)
        {
            return _mapper.Map<ProdutoViewModel>(_produtoRepository.ObterPorId(id));
        }

        public async Task Adicionar(ProdutoViewModel produtoViewModel)
        {
            var command = _mapper.Map<AdicionarProdutoCommand>(produtoViewModel);

            if (!command.IsValid())
            {
                foreach (var error in command.ValidationResult.Errors)
                {
                    await _mediatorHandler.PublicarDomainNotification(new DomainNotification(command.MessageType,
                        error.ErrorMessage));
                }
                return;
            }

            await _mediatorHandler.EnviarCommand(command);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
