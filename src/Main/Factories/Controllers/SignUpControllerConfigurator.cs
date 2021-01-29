using System;
using Domain.UseCases;
using Main.Factories.Validators;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Controllers.SignUp;

namespace Main.Factories.Controllers
{
    public static class SignUpControllerConfigurator
    {
        public static IServiceCollection ConfigureSignUpController(this IServiceCollection services) => services.AddTransient(sp => CreateSignupController(sp));
        public static SignUpController CreateSignupController(IServiceProvider servicesProvider)
        {
            var addAccount = servicesProvider.GetService<IAddAccount>();
            var validator = SignUpValidatorBuilder.Create();
            var controller = new SignUpController(validator, addAccount);
            return controller;
        }
    }
}
