using ApexaAdvisors.Domain.Contracts;
using ApexaAdvisors.Domain.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace ApexaAdvisors.Ef.Repositories;

public class AdvisorQueryRepository : IAdvisorQueryRepository
{
    private readonly AppDbContext _dbContext;

    public AdvisorQueryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<AdvisorListResponse>> GetList(AdvisorSearchPrameters parameters)
    {
        var query = _dbContext.Advisors.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(parameters.Name))
            query = query.Where(a => a.Name.Contains(parameters.Name));

        if (!string.IsNullOrWhiteSpace(parameters.Sin))
            query = query.Where(a => a.Sin.Contains(parameters.Sin));

        if (parameters.HealthStatus is not null)
            query = query.Where(a => a.HealthStatus == parameters.HealthStatus);

        query = query.Skip((parameters.Page - 1) * parameters.PageSize).Take(parameters.PageSize);

        return await query
            .Select(q => new AdvisorListResponse
            {
                Id = q.Id,
                Name = q.Name,
                Sin = q.Sin,
                HealthStatus = q.HealthStatus,
            })
            .ToListAsync();
    }

    public async Task<AdvisorDetailsResponse?> GetDetails(Guid id)
    {
        return await _dbContext.Advisors
            .AsNoTracking()
            .Select(q => new AdvisorDetailsResponse
            {
                Id = q.Id,
                Name = q.Name,
                Sin = q.Sin,
                Address = q.ContactInformation.Address,
                Phone = q.ContactInformation.Phone,
                HealthStatus = q.HealthStatus,
            })
            .SingleOrDefaultAsync();

    }
}
