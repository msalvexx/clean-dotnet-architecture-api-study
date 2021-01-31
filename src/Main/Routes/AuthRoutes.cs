using Main.Adapters;
using Main.Helpers;
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
            var controller = Helper.GetRegisteredService<IController<ISignUpRequest>>(app);
            endpoints.MapPost("signup", controller.AdaptRoute());
            return endpoints;
        }
    }
}
