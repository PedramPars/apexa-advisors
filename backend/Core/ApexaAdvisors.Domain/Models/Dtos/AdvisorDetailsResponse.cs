using ApexaAdvisors.Domain.Models.Entities;
using ApexaAdvisors.Domain.Models.Enums;

namespace ApexaAdvisors.Domain.Models.Dtos;

public record AdvisorDetailsResponse
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Sin { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public required HealthStatus HealthStatus { get; set; }

    public static AdvisorDetailsResponse From(Advisor advisor) => new()
    {
        Id = advisor.Id,
        Name = advisor.Name,
        Sin = advisor.Sin,
        Address = advisor.ContactInformation.Address,
        Phone = advisor.ContactInformation.Phone,
        HealthStatus = advisor.HealthStatus,
    };
}
