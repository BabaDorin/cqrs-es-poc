using Domain.Enums;

namespace Application.Models
{
    public class ShippingOrderDto
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public OrderStatus Status { get; set; }
    }
}
