using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Valantis.Catalogo.Application.Interfaces;
using Valantis.Catalogo.Application.ViewModels;
using Valantis.Catalogo.Domain.Messaging;

namespace Valantis.Catalogo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoAppService _produtoAppService;
        private readonly DomainNotificationHandler _notifications;

        public ProdutoController(IProdutoAppService produtoAppService,
                                 INotificationHandler<DomainNotification> notifications)
        {
            _produtoAppService = produtoAppService;
            _notifications = (DomainNotificationHandler)notifications;
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

        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] ProdutoViewModel produtoViewModel)
        {
            await _produtoAppService.Adicionar(produtoViewModel);

            if (!_notifications.HasNotifications())
            {
                return Ok();
            }

            return UnprocessableEntity(new
            {
                errors = _notifications.GetNotifications().Select(c => c.Value)
            });
        }
    }
}
