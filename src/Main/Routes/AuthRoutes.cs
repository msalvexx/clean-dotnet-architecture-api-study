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
            endpoints.MapPost("signup", GetController<SignUpController>(app).AdaptRoute<SignUpRequest>());
            return endpoints;
        }

        private static TController GetController<TController>(IApplicationBuilder app) where TController : IController =>
            (TController)app.ApplicationServices.GetService(typeof(TController));
    }
}
