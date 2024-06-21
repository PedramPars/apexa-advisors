using ApexaAdvisors.Domain.Contracts;
using ApexaAdvisors.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApexaAdvisors.Ef.Repositories;

public class AdvisorCommandRepository : IAdvisorCommandRepository
{
    private readonly AppDbContext _dbContext;

    public AdvisorCommandRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Advisor?> GetById(Guid id)
    {
        return await _dbContext.Advisors.FindAsync(id);
    }

    public async Task<Advisor?> GetBySin(string sin)
    {
        return await _dbContext.Advisors.SingleOrDefaultAsync(s => s.Sin == sin);
    }

    public async Task Create(Advisor advisor)
    {
        await _dbContext.AddAsync(advisor);
    }

    public void Update(Advisor advisor)
    {
        _dbContext.Attach(advisor);
    }

    public async Task Delete(Guid id)
    {
        var advisor = await GetById(id);
        if (advisor is not null)
            Delete(advisor);
    }

    public void Delete(Advisor advisor)
    {
        _dbContext.Remove(advisor);
    }
}
