using Infrastructure.Configurations.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Infrastructure.Configurations
{
    public static class ConfigurationHelper
    {
        public static T GetConfiguration<T>(this IConfiguration configuration) where T : class
        {
            var typeName = typeof(T).Name;
            var existsChildren = configuration.GetChildren().Any(item => item.Key == typeName);

            var message = $"Configuration item not found for type {typeof(T).FullName}";

            if (!existsChildren)
            {
                throw new Exception(message);
            }

            var model = configuration.GetSection(typeName).Get<T>();

            if (model is null)
            {
                throw new InvalidOperationException(message);
            }

            return model;
        }
    }
}
