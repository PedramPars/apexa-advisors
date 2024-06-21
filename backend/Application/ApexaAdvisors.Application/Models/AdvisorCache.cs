using ApexaAdvisors.Domain.Models.Dtos;

namespace ApexaAdvisors.Application.Models;

public record AdvisorCache
{
    public required DateTime StoreDate { get; set; }
    public required AdvisorDetailsResponse Advisor { get; set; }
}
