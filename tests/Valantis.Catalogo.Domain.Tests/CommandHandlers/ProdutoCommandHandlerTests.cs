using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.AutoMock;
using Valantis.Catalogo.Domain.CommandHandlers;
using Valantis.Catalogo.Domain.Commands.ProdutoCommands;
using Valantis.Catalogo.Domain.Entities;
using Valantis.Catalogo.Domain.Interfaces;
using Xunit;

namespace Valantis.Catalogo.Domain.Tests.CommandHandlers
{
    public class ProdutoCommandHandlerTests
    {
        private readonly AutoMocker _mocker;
        private readonly ProdutoCommandHandler _produtoCommandHandler;

        public ProdutoCommandHandlerTests()
        {
            _mocker = new AutoMocker();
            _produtoCommandHandler = _mocker.CreateInstance<ProdutoCommandHandler>();
        }

        [Fact(DisplayName = "AdicionarProduto_DeveExecutarComSucesso")]
        [Trait("Categoria", "Catálogo - ProdutoCommandHandler")]
        public async Task AdicionarProduto_DeveExecutarComSucesso()
        {
            // Arrange
            var produtoCommand = new AdicionarProdutoCommand("Produto 1");

            // Act
            var result = await _produtoCommandHandler.Handle(produtoCommand, CancellationToken.None);

            // Assert
            Assert.True(result);
            _mocker.GetMock<IProdutoRepository>().Verify(e => 
                e.Adicionar(It.IsAny<Produto>()), Times.Once);
        }

        [Fact(DisplayName = "AdicionarProduto_DevePublicarDomainNotification_QuandoNomeVazio")]
        [Trait("Categoria", "Catálogo - ProdutoCommandHandler")]
        public async Task AdicionarProduto_DevePublicarDomainNotification_QuandoNomeVazio()
        {
            // Arrange
            var produtoCommand = new AdicionarProdutoCommand("");

            // Act
            var result = await _produtoCommandHandler.Handle(produtoCommand, CancellationToken.None);

            // Assert
            Assert.False(result);
            _mocker.GetMock<IProdutoRepository>().Verify(e =>
                e.Adicionar(It.IsAny<Produto>()), Times.Never);
        }
    }
}
