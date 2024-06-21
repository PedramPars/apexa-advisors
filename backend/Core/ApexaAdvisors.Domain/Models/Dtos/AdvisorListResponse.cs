using ApexaAdvisors.Domain.Models.Enums;

namespace ApexaAdvisors.Domain.Models.Dtos;

public record AdvisorListResponse
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Sin { get; set; }
    public required HealthStatus HealthStatus { get; set; }
}
