using ApexaAdvisors.Domain.Models.Enums;

namespace ApexaAdvisors.Application.Services;

public class HealthGenerator
{
    public HealthStatus Generate()
    {
        var random = new Random();
        var randomValue = random.Next(1, 101);

        return randomValue switch
        {
            <= 60 => HealthStatus.Green,
            <= 80 => HealthStatus.Yellow,
            _ => HealthStatus.Red
        };
    }
}
