using Microsoft.Extensions.Configuration;

namespace Infrastructure.Configurations
{
    public static class ConfigurationHelper
    {
        public static T GetConfiguration<T>(this IConfiguration configuration)
        {
            var typeName = typeof(T).Name;
            var ExistsChildren = configuration.GetChildren().Any(item => item.Key == typeName);

            if (ExistsChildren)
            {
                configuration = configuration.GetSection(typeName);
            }

            T model = configuration.GetConfiguration<T>();

            if (model == null)
            {
                var message = $"Configuration item not found for type {typeof(T).FullName}";
                throw new InvalidOperationException(message);
            }

            return model;
        }
    }
}
