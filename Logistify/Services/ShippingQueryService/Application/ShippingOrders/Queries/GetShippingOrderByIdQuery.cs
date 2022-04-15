using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.ShippingOrders.Queries
{
    public class GetShippingOrderByIdQuery : IRequest<ShippingOrderDetails>
    {
        public GetShippingOrderByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }

    public class GetShippingOrderByIdQueryHandler : IRequestHandler<GetShippingOrderByIdQuery, ShippingOrderDetails>
    {
        private readonly IShippingOrdersRespository ordersRespository;

        public GetShippingOrderByIdQueryHandler(IShippingOrdersRespository ordersRespository)
        {
            this.ordersRespository = ordersRespository;
        }

        public async Task<ShippingOrderDetails> Handle(GetShippingOrderByIdQuery request, CancellationToken cancellationToken)
        {
            return await ordersRespository.GetByIdAsync(request.Id, cancellationToken);
        }
    }
}