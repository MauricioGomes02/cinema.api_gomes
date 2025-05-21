namespace Domain.Ports
{
    public interface IEventBusPort : IDisposable
    {
        Task PublishAsync<TEvent>(TEvent @event) where TEvent: class;
        Task SubscribeAsync<TEvent>(Func<TEvent, Task> func) where TEvent : class;
        Task UnSubscribeAsync<TEvent, TEventHandler>()
            where TEvent : class
            where TEventHandler : IEventHandler;
    }

    public interface IEventHandler
    {
        Task HandleAsync<TEvent>(TEvent @event) where TEvent: class;
    }
}
