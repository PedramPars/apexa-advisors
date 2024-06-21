using ApexaAdvisors.Application.Services;
using ApexaAdvisors.Domain.Contracts;
using ApexaAdvisors.Domain.Models.Dtos;

namespace ApexaAdvisors.Pipelines;

public static class ConfigurationPipeline
{
    public static WebApplicationBuilder AddConfigurations(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<ApplicationSettings>(builder.Configuration.GetSection(nameof(ApplicationSettings)));

        return builder;
    }
}
