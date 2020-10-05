using Microsoft.Extensions.DependencyInjection;
using System;
using Valantis.Catalogo.Infra.CrossCutting.IoC;

namespace Valantis.Catalogo.Api.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}
