using ApexaAdvisors.Domain.Models.Dtos;

namespace ApexaAdvisors.Domain.Contracts;

public interface IAdvisorQueryRepository
{
    Task<IEnumerable<AdvisorListResponse>> GetList(AdvisorSearchPrameters parameters);
    Task<AdvisorDetailsResponse?> GetDetails(Guid id);
}
