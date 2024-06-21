using ApexaAdvisors.Domain.Models.Entities;

namespace ApexaAdvisors.Domain.Contracts;

public interface IAdvisorCommandRepository
{
    Task<Advisor?> GetById(Guid id);
    Task<Advisor?> GetBySin(string sin);
    Task Create(Advisor advisor);
    void Update(Advisor advisor);
    Task Delete(Guid id);
    void Delete(Advisor advisor);
}
