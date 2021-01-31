using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;

namespace Main.Helpers
{
    public static class Helper
    {
        public static TClass GetRegisteredService<TClass>(IApplicationBuilder app) where TClass : class
        {
            var concreteClassType = GetConcreteClass<TClass>();
            return (TClass)app.ApplicationServices.GetService(concreteClassType);
        }

        public static Type GetConcreteClass<TClass>() where TClass : class
        {
            var typeInterface = typeof(TClass);
            var concreteClassType = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeInterface.IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract)
                .FirstOrDefault();
            return concreteClassType;
        }
    }
}
