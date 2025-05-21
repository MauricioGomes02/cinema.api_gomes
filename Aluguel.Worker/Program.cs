using Aluguel.Worker;
using Application;
using Domain.Ports;
using Domain.Services;
using Infrastructure.Adapters;
using Infrastructure.Configurations;
using Infrastructure.Configurations.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services
    .Configure<MongoDbAdapterConfigurationModel>(builder.Configuration.GetSection("MongoDb"))
    .Configure<RabbitMqAdapterConfigurationModel>(builder.Configuration.GetSection("RabbitMq"))
    .AddSingleton<IMongoClient>(x =>
    {
        var options = x.GetRequiredService<IOptions<MongoDbAdapterConfigurationModel>>();
        var settings = MongoClientSettings.FromConnectionString(options.Value.ConnectionString);
        return new MongoClient(options.Value.ConnectionString);
    });

BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));

builder.Services
    .AddScoped<IRentalPort, RentalAdapter>()
    .AddScoped<IRentalService, RentalService>()
    .AddScoped<IEventBusPort, RabbitMqAdapter>();

builder.Services.AddIMDb(builder.Configuration);

var host = builder.Build();
host.Run();
