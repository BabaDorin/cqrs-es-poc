
using Infrastructure.Abstractions;
using Infrastructure.Services;
using Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presentation;

namespace Infrastructure.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddGrpcClients(this IServiceCollection services, ConfigurationManager configuration)
        {
            var grpcClientSettings = (GrpcClientSettings)configuration.GetSection("GrpcClientSettings");

            services.AddGrpcClient<ShippingQueryService.ShippingQueryServiceClient>((services, options) =>
            {
                options.Address = new Uri(grpcClientSettings.ShippingQuery);
            });

            services.AddSingleton<IShippingQueryServiceClient, ShippingQueryServiceClient>();

            return services;
        }
    }
}
