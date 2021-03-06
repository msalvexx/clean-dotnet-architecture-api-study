using System;
using System.Threading.Tasks;
using Main.Helpers;
using Microsoft.AspNetCore.Http;
using Presentation.Protocols;

namespace Main.Adapters
{
    public static class AdaptRouterConfigurator
    {
        public static RequestDelegate AdaptRoute<TRequest>(this IController<TRequest> controller) where TRequest : class =>
            async context =>
            {
                if (!context.Request.HasJsonContentType())
                {
                    context.Response.StatusCode = StatusCodes.Status415UnsupportedMediaType;
                    return;
                }
                var request = await MapToRequest<TRequest>(context);
                var response = await controller.HandleAsync(request);
                context.Response.StatusCode = response.Status;
                if (response.Status is >= StatusCodes.Status200OK and < StatusCodes.Status300MultipleChoices)
                {
                    await context.Response.WriteAsJsonAsync(response.Body);
                    return;
                }
                else
                {
                    await context.Response.WriteAsJsonAsync(new
                    {
                        ((Exception)response.Body).Message
                    });
                }
            };

        public static async Task<HttpRequest<TRequest>> MapToRequest<TRequest>(HttpContext context) where TRequest : class
        {
            var concreteClassType = Helper.GetConcreteClass<TRequest>();
            return new HttpRequest<TRequest>
            {
                Body = (TRequest)await context.Request.ReadFromJsonAsync(concreteClassType)
            };
        }
    }
}
