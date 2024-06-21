using ApexaAdvisors.Domain.Models.Enums;

namespace ApexaAdvisors.Domain.Models.Dtos;

public class AdvisorSearchPrameters
{
    public string? Name { get; set; }
    public string? Sin { get; set; }
    public HealthStatus? HealthStatus { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
