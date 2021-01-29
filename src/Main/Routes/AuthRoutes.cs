using Main.Adapters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Presentation.Controllers.SignUp;
using Presentation.Protocols;

namespace Main.Routes
{
    public static class AuthRoutesConfigurator
    {
        public static IEndpointRouteBuilder MapAuthRoutes(this IEndpointRouteBuilder endpoints, IApplicationBuilder app)
        {
            var controller = GetController<ISignUpRequest>(app);
            endpoints.MapPost("signup", controller.AdaptRoute());
            return endpoints;
        }

        private static IController<TRequest> GetController<TRequest>(IApplicationBuilder app) where TRequest : class
            => (IController<TRequest>)app.ApplicationServices.GetService(typeof(IController<TRequest>));
    }
}
