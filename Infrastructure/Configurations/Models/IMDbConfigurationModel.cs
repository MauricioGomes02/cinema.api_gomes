using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Configurations.Models
{
    public class IMDbConfigurationModel
    {
        public string Url { get; set; }
        public string ApiKey { get; set; }
        public string Language { get; set; }
    }
}
