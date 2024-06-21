namespace ApexaAdvisors.Domain.Contracts;

public interface IUnitOfWork
{
    Task Commit();
}
