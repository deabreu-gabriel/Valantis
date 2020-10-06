using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Valantis.Catalogo.Application.Interfaces;
using Valantis.Catalogo.Application.Services;
using Valantis.Catalogo.Domain.CommandHandlers;
using Valantis.Catalogo.Domain.Commands.ProdutoCommands;
using Valantis.Catalogo.Domain.Interfaces;
using Valantis.Catalogo.Domain.Mediator;
using Valantis.Catalogo.Domain.Messaging;
using Valantis.Catalogo.Infra.Data.Repository;

namespace Valantis.Catalogo.Infra.CrossCutting.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Application
            services.AddScoped<IProdutoAppService, ProdutoAppService>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<AdicionarProdutoCommand, bool>, ProdutoCommandHandler>();

            // Infra - Data
            services.AddScoped<IProdutoRepository, ProdutoRepository>();

        }
    }
}
