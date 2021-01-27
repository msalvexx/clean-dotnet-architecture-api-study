using System;
using Factories.Controllers;
using Main.Adapters;
using Main.Routes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation.Controllers.SignUp;

namespace Main
{
    public class Startup
    {
        private IServiceCollection services;

        public void ConfigureServices(IServiceCollection services)
        {
            this.services = services;
            services.ConfigureSignUpController();
        }

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
