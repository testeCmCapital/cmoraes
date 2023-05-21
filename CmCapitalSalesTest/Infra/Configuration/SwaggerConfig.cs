using Microsoft.OpenApi.Models;

namespace CmCapitalSalesAvaliacao.Infra.Configuration
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "Avaliação Capital Markets API",
                        Description = "Api de cenário de controle de compras para teste de desenvolvimento",
                        Version = "v1"
                    });
                }
            );
        }
    }
}
