namespace Domain.Abstractions
{
    public interface IEvent
    {
        int Version { get; set; }
    }
}
