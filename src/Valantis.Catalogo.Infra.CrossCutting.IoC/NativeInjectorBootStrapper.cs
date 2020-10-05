using Microsoft.Extensions.DependencyInjection;
using Valantis.Catalogo.Application.Interfaces;
using Valantis.Catalogo.Application.Services;
using Valantis.Catalogo.Domain.Interfaces;
using Valantis.Catalogo.Infra.Data.Repositories;

namespace Valantis.Catalogo.Infra.CrossCutting.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Application
            services.AddScoped<IProdutoAppService, ProdutoAppService>();

            // Infra - Data
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
        }
    }
}
