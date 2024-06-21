using ApexaAdvisors.Domain.Contracts;
using ApexaAdvisors.Domain.Models.Entities;

namespace ApexaAdvisors.Domain.Events;

public record AdvisorCreatedOrUpdatedDomainEvent(Advisor Advisor) : IDomainEvent;
