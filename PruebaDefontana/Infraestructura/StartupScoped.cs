using Microsoft.Extensions.DependencyInjection;
using PruebaDefontana.Interfaz;
using PruebaDefontana.Servicio;

namespace PruebaDefontana.Infraestructura
{
    public static class StartupScoped
    {
        public static void ConfigureScoped(this IServiceCollection services)
        {
            services.AddScoped<ITest, TestService>();
        }
    }
}
