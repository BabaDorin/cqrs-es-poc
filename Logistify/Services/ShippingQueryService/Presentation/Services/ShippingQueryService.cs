using Application.ShippingOrders.Queries;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Services
{
    public class ShippingOrderQueryService : ShippingQueryService.ShippingQueryServiceBase 
    {
        private readonly IMediator mediator;

        public ShippingOrderQueryService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public override async Task<ShippingOrderDetails> GetShippingOrderById(
            GetShippingOrderByIdRequest request, ServerCallContext context)
        {
            var query = new GetShippingOrderByIdQuery(Guid.Parse(request.Id));
            var result = await mediator.Send(query, context.CancellationToken);

            var response = new ShippingOrderDetails
            {
                Id = result.Id.ToString(),
                Address = result.Address,
                Description = result.Description,
                Status = (OrderStatus)result.Status,
                PlacedBy = result.PlacedBy
            };

            return response;
        }

        public override async Task<GetShippingOrdersResponse> GetShippingOrders(
            Empty request, ServerCallContext context)
        {
            var query = new GetShippingOrdersQuery();
            var result = await mediator.Send(query, context.CancellationToken);

            var response = new GetShippingOrdersResponse();

            if (result != null && result.Any())
            {
                var shippingOrders = result.Select(order => new ShippingOrder
                {
                    Id = order.Id.ToString(),
                    Address = order.Address,
                    Status = (OrderStatus)order.Status,
                });

                response.Orders.AddRange(shippingOrders);
            }

            return response;
        }

        public override Task<PublishMessageSimulationResponse> PublishMessageSimulation(ShippingOrderEventMessage request, ServerCallContext context)
        {
            return base.PublishMessageSimulation(request, context);
        }
    }
}
