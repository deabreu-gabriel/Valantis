using Microsoft.AspNetCore.Mvc;
using Moq.AutoMock;
using System.Collections.Generic;
using System.Threading.Tasks;
using Valantis.Catalogo.Api.Controllers;
using Valantis.Catalogo.Application.Interfaces;
using Valantis.Catalogo.Application.ViewModels;
using Valantis.Catalogo.Domain.Messaging;
using Xunit;

namespace Valantis.Catalogo.Api.Tests.Controllers
{
    public class ProdutoControllerTests
    {
        private readonly AutoMocker _mocker;
        private readonly ProdutoController _produtoController;

        public ProdutoControllerTests()
        {
            _mocker = new AutoMocker();

            var produtoAppService = _mocker.GetMock<IProdutoAppService>();
            var notifications = _mocker.GetMock<DomainNotificationHandler>();

            _produtoController = new ProdutoController(produtoAppService.Object, notifications.Object);
        }

        [Fact(DisplayName = "AdicionarProduto_DeveRetornarOk")]
        [Trait("Categoria", "Catálogo - ProdutoController")]
        public async Task AdicionarProduto_DeveRetornarOk()
        {
            // Arrange
            var produtoViewModel = new ProdutoViewModel
            {
                Nome = "Produto 1"
            };

            _mocker.GetMock<DomainNotificationHandler>().Setup(e => 
                e.HasNotifications()).Returns(false);

            // Act
            var result = await _produtoController.Adicionar(produtoViewModel);
            var okResult = result as OkResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact(DisplayName = "AdicionarProduto_DeveRetornarUnprocessableEntity")]
        [Trait("Categoria", "Catálogo - ProdutoController")]
        public async Task AdicionarProduto_DeveRetornarUnprocessableEntity()
        {
            // Arrange
            var produtoViewModel = new ProdutoViewModel
            {
                Nome = "Produto 1"
            };

            var notifications = new List<DomainNotification>()
            {
                new DomainNotification("AdicionarProdutoCommand", "Falhou no Teste.")
            };

            _mocker.GetMock<DomainNotificationHandler>().Setup(e =>
                e.HasNotifications()).Returns(true);

            _mocker.GetMock<DomainNotificationHandler>().Setup(e =>
                e.GetNotifications()).Returns(notifications);

            // Act
            var result = await _produtoController.Adicionar(produtoViewModel);

            // Assert
            var unprocessableEntityResultResult = result as UnprocessableEntityObjectResult;
            Assert.NotNull(unprocessableEntityResultResult);
            Assert.Equal(422, unprocessableEntityResultResult.StatusCode);

        }
    }
}
