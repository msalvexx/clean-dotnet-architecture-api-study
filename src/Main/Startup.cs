using Main.Factories.Controllers;
using Main.Routes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Main
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services) => services.ConfigureSignUpController();

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapAuthRoutes(app));
        }
    }
}
