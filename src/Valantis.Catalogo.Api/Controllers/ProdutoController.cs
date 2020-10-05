using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Valantis.Catalogo.Application;
using Valantis.Catalogo.Application.Interfaces;
using Valantis.Catalogo.Application.ViewModels;

namespace Valantis.Catalogo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoAppService _produtoAppService;

        public ProdutoController(IProdutoAppService produtoAppService)
        {
            _produtoAppService = produtoAppService;
        }

        [HttpGet]
        public IEnumerable<ProdutoViewModel> Get()
        {
            return _produtoAppService.ObterTodos();
        }

        [HttpGet("{id:guid}")]
        public ProdutoViewModel Get(Guid id)
        {
            return _produtoAppService.ObterPorId(id);
        }
    }
}
