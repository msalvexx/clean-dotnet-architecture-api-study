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
            var validator = SignUpValidatorBuilder.Create();
            var controller = new SignUpController(validator, null);
            return controller;
        }
    }
}
