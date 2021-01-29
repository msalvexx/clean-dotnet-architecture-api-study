using Data.UseCases;
using Infra.Adapters;
using Infra.Db.MongoDb.Configurators;
using Infra.Db.MongoDb.Models;
using Infra.Db.MongoDb.Repositories;
using Main.Factories.Validators;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Controllers.SignUp;

namespace Main.Factories.Controllers
{
    public static class SignUpControllerConfigurator
    {
        public static IServiceCollection ConfigureSignUpController(this IServiceCollection services) => services.AddTransient(sp => CreateSignupController());
        public static SignUpController CreateSignupController()
        {
            var settings = new MongoDbSettings();
            var context = new MongoDbContext(settings);
            var repo = new AccountMongoRepository(context);
            var hasher = new BcryptAdapter();
            var addAccount = new DbAddAccount(hasher, repo);
            var validator = SignUpValidatorBuilder.Create();
            var controller = new SignUpController(validator, addAccount);
            return controller;
        }
    }
}
