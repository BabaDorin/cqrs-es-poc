using Domain.Enums;

namespace Application.Models
{
    public class ShippingOrderDetailsDto
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public OrderStatus Status { get; set; }
        public string PlacedBy { get; set; }
        public string Description { get; set; }
    }
}
