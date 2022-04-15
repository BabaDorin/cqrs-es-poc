using Domain.Enums;

namespace Domain.Entities
{
    public class ShippingOrder
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public OrderStatus Status { get; set; }
    }
}
