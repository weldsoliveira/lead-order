using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using MKT.EventoLead.Domain.Interfaces.Repository;
using MKT.EventoLead.Infra.Context;
using MKT.EventoLead.Infra.Repository;

namespace MKT.EventoLead.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void ResolveDependencies(this IServiceCollection services)
        {
            services.AddDbContext<EventoLeadContext>(options => options.UseSqlServer("name=ConnectionStrings:EventoLead"));
            services.AddDbContext<B2BEuropeContext>(options => options.UseSqlServer("name=ConnectionStrings:B2BEurope"));

            services.AddScoped<ILeadRepository, LeadRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            // Configuração do limite de requisição
            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 524288000; // 500 MB
            });

            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 524288000; // 500 MB
            });


            //services.Configure<FormOptions>(options =>
            //{
            //    options.MultipartBodyLengthLimit = 10004857600; // 100 MB (ajuste conforme necessário)
            //});

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
        }
    }
}
