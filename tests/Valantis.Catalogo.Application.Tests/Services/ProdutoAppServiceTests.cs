using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Moq.AutoMock;
using Valantis.Catalogo.Application.AutoMapper;
using Valantis.Catalogo.Application.Services;
using Valantis.Catalogo.Application.ViewModels;
using Valantis.Catalogo.Domain.Commands.ProdutoCommands;
using Valantis.Catalogo.Domain.Entities;
using Valantis.Catalogo.Domain.Interfaces;
using Valantis.Catalogo.Domain.Mediator;
using Valantis.Catalogo.Domain.Messaging;
using Xunit;

namespace Valantis.Catalogo.Application.Tests.Services
{
    public class ProdutoAppServiceTests
    {
        private readonly AutoMocker _mocker;
        private readonly ProdutoAppService _produtoAppService;

        public ProdutoAppServiceTests()
        {
            _mocker = new AutoMocker();

            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainToViewModelMappingProfile());
                cfg.AddProfile(new ViewModelToDomainMappingProfile());
            }).CreateMapper();
            
            var produtoRepository = _mocker.GetMock<IProdutoRepository>();
            var mediatorHandler = _mocker.GetMock<IMediatorHandler>();

            _produtoAppService = new ProdutoAppService(mapper, mediatorHandler.Object, produtoRepository.Object); 
        }

        [Fact(DisplayName = "ObterTodosProdutos_DeveExecutarComSucesso")]
        [Trait("Categoria", "Catálogo - ProdutoAppService")]
        public void ObterTodosProdutos_DeveExecutarComSucesso()
        {
            // Arrange
            var produtos = new List<Produto>()
            {
                new Produto(Guid.NewGuid(),  "Produto 1"),
                new Produto(Guid.NewGuid(),  "Produto 2"),
                new Produto(Guid.NewGuid(),  "Produto 3")
            };

            _mocker.GetMock<IProdutoRepository>().Setup(r => r.ObterTodos()).Returns(produtos);

            // Act
            var result = _produtoAppService.ObterTodos();

            // Assert
            _mocker.GetMock<IProdutoRepository>().Verify(r => r.ObterTodos(), Times.Once);
            Assert.Equal(3, result.Count());
        }

        [Fact(DisplayName = "ObterProdutoPorId_DeveExecutarComSucesso")]
        [Trait("Categoria", "Catálogo - ProdutoAppService")]
        public void ObterProdutoPorId_DeveExecutarComSucesso()
        {
            // Arrange
            var produto = new Produto(Guid.NewGuid(), "Produto 1");

            _mocker.GetMock<IProdutoRepository>().Setup(r => r.ObterPorId(produto.Id)).Returns(produto);

            // Act
            var result = _produtoAppService.ObterPorId(produto.Id);

            // Assert
            _mocker.GetMock<IProdutoRepository>().Verify(r => r.ObterPorId(produto.Id), Times.Once);

            Assert.NotNull(result);
            Assert.Equal(produto.Id, result.Id);
            Assert.Equal(produto.Nome, result.Nome);
        }

        [Fact(DisplayName = "AdicionarProduto_DevePublicarDomainNotification_QuandoNomeVazio")]
        [Trait("Categoria", "Catálogo - ProdutoAppService")]
        public async Task AdicionarProduto_DevePublicarDomainNotification_QuandoNomeVazio()
        {
            // Arrange
            var produtoViewModel = new ProdutoViewModel
            {
                Nome = ""
            };

            // Act
            await _produtoAppService.Adicionar(produtoViewModel);

            // Assert
            _mocker.GetMock<IMediatorHandler>().Verify(r => 
                r.PublicarDomainNotification(
                    It.Is<DomainNotification>(dm => dm.Value == "Por favor, tenha certeza que informou o Nome.")),
                    Times.Once);
        }

        [Fact(DisplayName = "AdicionarProduto_DeveEnviarCommand_QuandoEstiverValido")]
        [Trait("Categoria", "Catálogo - ProdutoAppService")]
        public async Task AdicionarProduto_DeveEnviarCommand_QuandoEstiverValido()
        {
            // Arrange
            var produtoViewModel = new ProdutoViewModel
            {
                Nome = "Produto 1"
            };

            // Act
            await _produtoAppService.Adicionar(produtoViewModel);

            // Assert
            _mocker.GetMock<IMediatorHandler>().Verify(r => 
                r.EnviarCommand(
                    It.Is<AdicionarProdutoCommand>(c => c.Nome == produtoViewModel.Nome)), 
                    Times.Once);
        }
    }
}
