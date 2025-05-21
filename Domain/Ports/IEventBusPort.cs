namespace Domain.Ports
{
    public interface IEventBusPort
    {
        Task PublishAsync<TEvent>(TEvent @event) where TEvent: class;
        Task SubscribeAsync<TEvent, TEventHandler>() 
            where TEvent: class 
            where TEventHandler: IEventHandler;
        Task UnSubscribeAsync<TEvent, TEventHandler>()
            where TEvent : class
            where TEventHandler : IEventHandler;
    }

    public interface IEventHandler
    {
        Task HandleAsync<TEvent>(TEvent @event) where TEvent: class;
    }
}
