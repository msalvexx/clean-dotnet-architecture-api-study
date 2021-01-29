using Data.Protocols;
using Infra.Adapters;
using Infra.Db.MongoDb.Configurators;
using Infra.Db.MongoDb.Models;
using Infra.Db.MongoDb.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Main.Factories.Infra
{
    public static class InfraConfigurator
    {
        public static IServiceCollection ConfigureInfra(this IServiceCollection services)
        {
            MongoDbMapper.Map();
            services.AddSingleton<IMongoDbSettings, MongoDbSettings>();
            services.AddTransient<MongoDbContext>();
            services.AddTransient<IAddAccountRepository, AccountMongoRepository>();
            services.AddTransient<IHasher, BcryptAdapter>();
            return services;
        }
    }
}
