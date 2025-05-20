using Application;
using Domain.Services;
using Infrastructure.Configurations;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Service
builder.Services.AddScoped<IMovieService, MovieService>();

//Adapter
//var tmdbAdapterConfiguration = configuration.GetConfiguration<TmdbAdapterConfiguration>();
//builder.Services.AddIMDbAdapter(tmdbAdapterConfiguration, configuration);
builder.Services.AddIMDb(configuration);

//var mongoAdapterConfiguration = configuration.GetConfiguration<MongoDbAdpterConfiguration>();
//builder.Services.AddMongo(mongoAdapterConfiguration);

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
