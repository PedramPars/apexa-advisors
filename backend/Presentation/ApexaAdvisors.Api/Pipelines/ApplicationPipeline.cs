using ApexaAdvisors.Application.Models;
using ApexaAdvisors.Application.Services;
using ApexaAdvisors.Domain.Contracts;
using ApexaAdvisors.Ef.Repositories;

namespace ApexaAdvisors.Pipelines;

public static class ApplicationPipeline
{
    public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddMemoryCache();

        builder.Services.AddScoped<IAdvisorQueryRepository, AdvisorQueryRepository>();
        builder.Services.AddScoped<IAdvisorCommandRepository, AdvisorCommandRepository>();

        builder.Services.AddSingleton<ICacheService, CacheService>();
        builder.Services.AddSingleton<IAdvisorCacheService, AdvisorCacheService>();
        builder.Services.AddTransient<HealthGenerator>();

        builder.Services.AddMediatR(m =>
        {
            m.RegisterServicesFromAssemblyContaining<Program>();
            m.RegisterServicesFromAssemblyContaining<Error>();
        });

        return builder;
    }
}
