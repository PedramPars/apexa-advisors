using ApexaAdvisors.Domain.Contracts;
using ApexaAdvisors.Domain.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApexaAdvisors.Ef;

public class AppDbContext : DbContext, IUnitOfWork
{
    private readonly IMediator _mediator;

    public AppDbContext(DbContextOptions options, IMediator mediator) : base(options)
    {
        _mediator = mediator;
    }

    public DbSet<Advisor> Advisors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Advisor>().Property(p => p.HealthStatus).HasConversion<string>();
        modelBuilder.Entity<Advisor>().HasIndex(p => p.Sin).IsUnique();
        modelBuilder.Entity<Advisor>().ComplexProperty(p => p.ContactInformation);
    }

    public async Task Migrate()
    {
        await Database.MigrateAsync();
    }

    public async Task Commit()
    {
        await DispatchDomainEventsAsync();
        await SaveChangesAsync();
    }

    public async Task DispatchDomainEventsAsync()
    {
        var domainEntities = ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.DomainEvents.Any());

        foreach (var domainEntity in domainEntities)
        {
            foreach (var domainEvent in domainEntity.Entity.DomainEvents)
                await _mediator.Publish(domainEvent);

            domainEntity.Entity.ClearDomainEvents();
        }
    }
}
