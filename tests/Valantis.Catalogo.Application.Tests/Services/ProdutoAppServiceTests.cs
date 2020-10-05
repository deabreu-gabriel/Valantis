using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Moq;
using Moq.AutoMock;
using Valantis.Catalogo.Application.AutoMapper;
using Valantis.Catalogo.Application.Services;
using Valantis.Catalogo.Domain.Entities;
using Valantis.Catalogo.Domain.Interfaces;
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

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile(new DomainToViewModelMappingProfile())).CreateMapper();
            var produtoRepository = _mocker.GetMock<IProdutoRepository>();

            _produtoAppService = new ProdutoAppService(mapper, produtoRepository.Object); 
        }

        [Fact(DisplayName = "ObterTodos deve obter e mapear produtos")]
        [Trait("Categoria", "Catálogo - ProdutoAppService")]
        public void ObterTodos_DeveObterEMapearProdutos()
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


        [Fact(DisplayName = "ObterPorId deve obter e mapear produto")]
        [Trait("Categoria", "Catálogo - ProdutoAppService")]
        public void ObterPorId_DeveObterEMapearProduto()
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
    }
}
