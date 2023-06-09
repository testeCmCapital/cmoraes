﻿using CmCapitalSalesAvaliacao.Domain.Services;
using CmCapitalSalesAvaliacao.Infra.Data;

namespace CmCapitalSalesAvaliacao.Infra.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<CmCapitalSalesDbContext>();
            services.AddScoped<CmCapitalSalesService>();
        }
    }
}
