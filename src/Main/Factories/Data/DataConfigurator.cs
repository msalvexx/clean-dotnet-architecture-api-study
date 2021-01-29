using Data.UseCases;
using Domain.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace Main.Factories.Data
{
    public static class DataConfigurator
    {
        public static IServiceCollection ConfigureData(this IServiceCollection services)
        {
            services.AddTransient<IAddAccount, DbAddAccount>();
            return services;
        }
    }
}
