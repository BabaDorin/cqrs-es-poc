using Application.Interfaces;
using Infrastructure.Services;
using Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ShippingCommandGrpc.Presentation;
using ShippingQueryGrpc.Presentation;

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

            services.AddGrpcClient<ShippingCommandService.ShippingCommandServiceClient>((services, options) =>
            {
                options.Address = new Uri(grpcClientSettings.Value.ShippingCommand);
            });

            services.AddSingleton<IShippingOrderQueryClient, ShippingQueryServiceClient>();
            services.AddSingleton<IShippingOrderCommandClient, ShippingCommandServiceClient>();

            return services;
        }
    }
}
