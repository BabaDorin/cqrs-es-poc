using Application.ShippingOrders.Commands;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Server.HttpSys;

namespace Presentation.Services
{
    public class ShippingOrderCommandService : ShippingCommandService.ShippingCommandServiceBase
    {
        private readonly IMediator mediator;

        public ShippingOrderCommandService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public override async Task<CommandResponse> CancelShippingOrder(
            ShippingOrderRequest request, ServerCallContext context)
        {
            var command = new CancelShippingOrderCommand(Guid.Parse(request.Id));
            var result = await mediator.Send(command, context.CancellationToken);

            var response = new CommandResponse { OperationSucceeded = result };

            return response;
        }

        public override async Task<ShippingOrderDetails> CreateShippingOrder(
            CreateShippingOrderRequest request, ServerCallContext context)
        {
            var command = new CreateShippingOrderCommand(request.Address, request.Description, request.PlacedBy);
            var result = await mediator.Send(command, context.CancellationToken);

            if (result is null) return null;

            var response = new ShippingOrderDetails
            {
                Id = result.Id.ToString(),
                Address = result.Address,
                Description = result.Description,
                PlacedBy = result.PlacedBy,
                Status = (OrderStatus)result.Status,
            };

            return response;
        }

        public override async Task<ShippingOrderDetails> UpdateShippingOrder(
            UpdateShippingOrderRequest request, ServerCallContext context)
        {
            var command = new UpdateShippingOrderCommand(Guid.Parse(request.Id), request.Address, request.Description);
            var result = await mediator.Send(command, context.CancellationToken);

            if (result is null) return null;

            var response = new ShippingOrderDetails
            {
                Id = result.Id.ToString(),
                Address = result.Address,
                Description = result.Description,
                PlacedBy = result.PlacedBy,
                Status = (OrderStatus)result.Status,
            };

            return response;
        }
    }
}