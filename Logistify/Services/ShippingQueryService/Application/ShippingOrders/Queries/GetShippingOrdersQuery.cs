using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.ShippingOrders.Queries
{
    public class GetShippingOrdersQuery : IRequest<IList<ShippingOrder>>
    {
    }

    public class GetShippingOrdersQueryHandler : IRequestHandler<GetShippingOrdersQuery, IList<ShippingOrder>>
    {
        private readonly IShippingOrdersRespository ordersRespository;

        public GetShippingOrdersQueryHandler(IShippingOrdersRespository ordersRespository)
        {
            this.ordersRespository = ordersRespository;
        }

        public async Task<IList<ShippingOrder>> Handle(GetShippingOrdersQuery request, CancellationToken cancellationToken)
        {
            return await ordersRespository.GetAsync(cancellationToken);
        }
    }
}
