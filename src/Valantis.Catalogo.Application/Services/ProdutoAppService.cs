using System;
using System.Collections.Generic;
using AutoMapper;
using Valantis.Catalogo.Application.Interfaces;
using Valantis.Catalogo.Application.ViewModels;
using Valantis.Catalogo.Domain;
using Valantis.Catalogo.Domain.Interfaces;

namespace Valantis.Catalogo.Application.Services
{
    public class ProdutoAppService : IProdutoAppService
    {
        private readonly IMapper _mapper;
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoAppService(IMapper mapper,
                                 IProdutoRepository produtoRepository)
        {
            _mapper = mapper;
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

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
