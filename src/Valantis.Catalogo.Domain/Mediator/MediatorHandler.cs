using MediatR;
using System.Threading.Tasks;
using Valantis.Catalogo.Domain.Messaging;

namespace Valantis.Catalogo.Domain.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task EnviarCommand<T>(T command) where T : Command
        {
            await _mediator.Send(command);
        }

        public async Task PublicarDomainNotification<T>(T notification) where T : DomainNotification
        {
            await _mediator.Publish(notification);
        }
    }
}
