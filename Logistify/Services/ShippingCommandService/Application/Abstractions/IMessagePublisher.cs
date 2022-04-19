using Domain.Abstractions;

namespace Application.Abstractions
{
    public interface IMessagePublisher
    {
        Task PublishAsync(Guid streamId, IEvent @event, CancellationToken cancellationToken);
    }
}
