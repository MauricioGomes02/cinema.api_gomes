namespace Infrastructure.Configurations.Models
{
    public class RabbitMqAdapterConfigurationModel
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public IEnumerable<Relationship> Relationships { get; set; }
    }

    public class Relationship
    {
        public string Exchange { get; set; }
        public IEnumerable<Queue> Queues { get; set; }
    }

    public class Queue
    {
        public string Name { get; set; }
        public string RoutingKey { get; set; }
    }
}
