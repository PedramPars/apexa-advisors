using System.Text.Json.Serialization;
using ApexaAdvisors.Pipelines;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddConfigurations();
builder.AddCustomEntityFrameworkCore();
builder.AddApplicationServices();

builder.Services.AddCors(c => c.AddDefaultPolicy(p => p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

builder.Services
    .AddControllers()
    .AddJsonOptions(o =>
    {
        var enumConverter = new JsonStringEnumConverter();
        o.JsonSerializerOptions.Converters.Add(enumConverter);
    });

var app = builder.Build();

await app.Seed();
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors();

app.UseRouting()
    .UseCors()
    .UseEndpoints(options =>
    {
        options.MapControllers();
    });

app.Run();
