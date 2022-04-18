using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.EventSourcing
{
    public static class Extensions
    {
        public static IServiceCollection AddEventResolver(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddSingleton<IEventResolver>(new EventResolver(assemblies));

            return services;
        }
    }
}
