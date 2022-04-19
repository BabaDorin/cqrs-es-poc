
using Infrastructure.Abstractions;
using Infrastructure.Services;
using Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Presentation;

namespace Infrastructure.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddGrpcClients(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.Configure<GrpcClientSettings>(configuration.GetSection("GrpcClientSettings"));

            var grpcClientSettings = services.BuildServiceProvider().GetService<IOptions<GrpcClientSettings>>();

            services.AddGrpcClient<ShippingQueryService.ShippingQueryServiceClient>((services, options) =>
            {
                options.Address = new Uri(grpcClientSettings.Value.ShippingQuery);
            });

            services.AddSingleton<IShippingQueryServiceClient, ShippingQueryServiceClient>();

            return services;
        }
    }
}
