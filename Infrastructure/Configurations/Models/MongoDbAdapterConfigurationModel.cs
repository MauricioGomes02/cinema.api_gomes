namespace Infrastructure.Configurations.Models
{
    public class MongoDbAdapterConfigurationModel
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}
