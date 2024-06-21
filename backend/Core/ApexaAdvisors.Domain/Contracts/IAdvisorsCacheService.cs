using ApexaAdvisors.Domain.Models.Dtos;

namespace ApexaAdvisors.Domain.Contracts;

public interface IAdvisorCacheService
{
    AdvisorDetailsResponse? Get(Guid id);
    void Add(AdvisorDetailsResponse advisor);
    void Update(AdvisorDetailsResponse advisor);
    void Delete(AdvisorDetailsResponse advisor);
}
