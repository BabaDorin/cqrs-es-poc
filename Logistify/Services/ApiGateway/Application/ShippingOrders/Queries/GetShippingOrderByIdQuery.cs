using Application.Interfaces;
using Application.Models;
using MediatR;

namespace Application.ShippingOrders.Queries
{
    public class GetShippingOrderByIdQuery : IRequest<ShippingOrderDetailsDto>
    {
        public GetShippingOrderByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }

    public class GetShippingOrderByIdQueryHandler : IRequestHandler<GetShippingOrderByIdQuery, ShippingOrderDetailsDto>
    {
        private readonly IShippingOrderQueryClient orderQueryClient;

        public GetShippingOrderByIdQueryHandler(IShippingOrderQueryClient orderQueryClient)
        {
            this.orderQueryClient = orderQueryClient;
        }

        public Task<ShippingOrderDetailsDto> Handle(GetShippingOrderByIdQuery request, CancellationToken cancellationToken)
        {
            return orderQueryClient.GetByIdAsync(request.Id, cancellationToken);
        }
    }
}
