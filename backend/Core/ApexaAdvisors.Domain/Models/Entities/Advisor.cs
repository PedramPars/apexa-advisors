using ApexaAdvisors.Domain.Contracts;
using ApexaAdvisors.Domain.CustomExceptions;
using ApexaAdvisors.Domain.Events;
using ApexaAdvisors.Domain.Models.Enums;
using ApexaAdvisors.Domain.Models.ValueObjects;
using ApexaAdvisors.Domain.Resources;

namespace ApexaAdvisors.Domain.Models.Entities;

public class Advisor : AggregateRoot
{
    protected Advisor()
    {
    }

    public Advisor(Guid? id, string name, string sin, ContactInformation? contactInformation = null)
    {
        var validations = Validate(name, sin);
        InvalidDomainStateException.ThrowIfAny(validations);

        Id = id ?? Guid.NewGuid();
        Name = name;
        Sin = sin;

        ContactInformation = contactInformation ?? ContactInformation.Empty;

        AddDomainEvent(new AdvisorCreatedOrUpdatedDomainEvent(this));
    }

    public string Name { get; private set; }
    public string Sin { get; private set; }
    public ContactInformation ContactInformation { get; private set; }
    public HealthStatus HealthStatus { get; private set; }

    public void SetName(string name)
    {
        Name = name;
        AddDomainEvent(new AdvisorCreatedOrUpdatedDomainEvent(this));
    }

    public void SetSin(string sin)
    {
        Sin = sin;
        AddDomainEvent(new AdvisorCreatedOrUpdatedDomainEvent(this));
    }

    public void SetHealthStatus(HealthStatus status)
    {
        HealthStatus = status;
        AddDomainEvent(new AdvisorCreatedOrUpdatedDomainEvent(this));
    }

    public void SetContactInformation(ContactInformation contactInformation)
    {
        ContactInformation = contactInformation;
        AddDomainEvent(new AdvisorCreatedOrUpdatedDomainEvent(this));
    }

    public static IEnumerable<string> Validate(string name, string sin)
    {
        if (name.Trim().Length > 255)
            yield return nameof(CommonResource.Validations_NameShouldBeLessThan255Characters);

        if (sin.Trim().Length != 9)
            yield return nameof(CommonResource.Validations_SinIsInvalid);
    }
}
