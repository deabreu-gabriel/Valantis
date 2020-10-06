using System.Threading.Tasks;
using Valantis.Catalogo.Domain.Messaging;

namespace Valantis.Catalogo.Domain.Mediator
{
    public interface IMediatorHandler
    {
        Task EnviarCommand<T>(T command) where T : Command;
        Task PublicarDomainNotification<T>(T notification) where T : DomainNotification;
    }
}
