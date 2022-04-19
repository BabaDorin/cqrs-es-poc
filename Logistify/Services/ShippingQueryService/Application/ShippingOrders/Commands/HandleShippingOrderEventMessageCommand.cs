using Application.Events;
using Application.EventSourcing.EsFramework;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Events;
using MediatR;
using System.Text.Json;

namespace Application.ShippingOrders.Commands
{
    public class HandleShippingOrderEventMessageCommand : IRequest
    {
        public HandleShippingOrderEventMessageCommand(ShippingOrderEventMessage eventMessage)
        {
            EventMessage = eventMessage;
        }

        public ShippingOrderEventMessage EventMessage { get; }
    }

    public class HandleShippingOrderEventMessageCommandHandler : IRequestHandler<HandleShippingOrderEventMessageCommand>
    {
        private readonly IShippingOrdersRespository ordersRespository;
        private readonly IEventResolver eventResolver;

        public HandleShippingOrderEventMessageCommandHandler(
            IShippingOrdersRespository ordersRespository,
            IEventResolver eventResolver)
        {
            this.ordersRespository = ordersRespository;
            this.eventResolver = eventResolver;
        }

        public async Task<Unit> Handle(HandleShippingOrderEventMessageCommand request, CancellationToken cancellationToken)
        {
            var streamId = request.EventMessage.StreamId;

            foreach (var ev in request.EventMessage.Events)
            {
                switch (ev.EventType)
                {
                    case nameof(ShippingOrderCreated):
                        await HandleOrderCreated(
                            streamId,
                            JsonSerializer.Deserialize<ShippingOrderCreated>(ev.Data),
                            cancellationToken);
                        break;
                    case nameof(ShippingOrderUpdated):
                        await HandleOrderUpdated(
                            streamId,
                            JsonSerializer.Deserialize<ShippingOrderUpdated>(ev.Data),
                            cancellationToken); 
                        break;
                    case nameof(ShippingOrderCanceled):
                        await HandleOrderCanceled(
                            streamId,
                            JsonSerializer.Deserialize<ShippingOrderCanceled>(ev.Data),
                            cancellationToken);
                        break;
                    default: 
                        throw new InvalidOperationException("Unknown Event type: " + ev.EventType);
                }
            }

            return Unit.Value;
        }

        private async Task HandleOrderCreated(Guid streamId, ShippingOrderCreated @event, CancellationToken cancellationToken)
        {
            // ShippingOrder Read Model
            var order = new ShippingOrder();
            order.Id = streamId;
            await eventResolver.Apply(@event, order);
            
            await ordersRespository.InsertAsync(order, cancellationToken);

            // ShippingOrderDetails Read Model
            var orderDetails = new ShippingOrderDetails();
            order.Id = streamId;
            await eventResolver.Apply(@event, orderDetails);

            await ordersRespository.InsertAsync(orderDetails, cancellationToken);
        }

        private async Task HandleOrderUpdated(Guid streamId, ShippingOrderUpdated @event, CancellationToken cancellationToken)
        {
            // ShippingOrder Read Model
            var order = await ordersRespository.GetByIdAsync(streamId, cancellationToken);

            await eventResolver.Apply(@event, order);
            await ordersRespository.UpdateAsync(order, cancellationToken);

            // ShippingOrderDetails Read Model
            var orderDetails = await ordersRespository.GetDetailsByIdAsync(streamId, cancellationToken);
            await eventResolver.Apply(@event, orderDetails);
            await ordersRespository.UpdateAsync(orderDetails, cancellationToken);
        }

        private async Task HandleOrderCanceled(Guid streamId, ShippingOrderCanceled @event, CancellationToken cancellationToken)
        {
            // ShippingOrder Read Model
            var order = await ordersRespository.GetByIdAsync(streamId, cancellationToken);

            await eventResolver.Apply(@event, order);
            await ordersRespository.UpdateAsync(order, cancellationToken);

            // ShippingOrderDetails Read Model
            var orderDetails = await ordersRespository.GetDetailsByIdAsync(streamId, cancellationToken);
            await eventResolver.Apply(@event, orderDetails);
            await ordersRespository.UpdateAsync(orderDetails, cancellationToken);
        }
    }
}
