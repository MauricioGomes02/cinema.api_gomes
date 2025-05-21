using Application;
using Domain.Ports;
using Domain.Services;
using Infrastructure.Adapters;
using Infrastructure.Configurations.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
    .AddScoped<IUserPort, UserMongoDbAdapter>()
    .AddScoped<IUserService, UserService>()
    .AddScoped<IEventBusPort, RabbitMqAdapter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
