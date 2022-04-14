using Application.Interfaces;
using Application.Models;
using MediatR;

namespace Application.ShippingOrders.Queries
{
    public class GetShippingOrdersQuery : IRequest<IList<ShippingOrderDto>>
    {
    }

    public class GetShippingOrdersQueryHandler : IRequestHandler<GetShippingOrdersQuery, IList<ShippingOrderDto>>
    {
        private readonly IShippingOrderQueryClient orderQueryClient;

        public GetShippingOrdersQueryHandler(IShippingOrderQueryClient orderQueryClient)
        {
            this.orderQueryClient = orderQueryClient;
        }

        public Task<IList<ShippingOrderDto>> Handle(GetShippingOrdersQuery request, CancellationToken cancellationToken)
        {
            return orderQueryClient.GetAsync(cancellationToken);
        }
    }
}
