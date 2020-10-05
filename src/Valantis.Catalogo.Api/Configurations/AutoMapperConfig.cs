using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using Valantis.Catalogo.Application.AutoMapper;

namespace Valantis.Catalogo.Api.Configurations
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(typeof(DomainToViewModelMappingProfile));
        }
    }
}
