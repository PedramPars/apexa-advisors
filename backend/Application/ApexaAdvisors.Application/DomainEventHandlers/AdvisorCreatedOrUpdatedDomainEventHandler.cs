using System.Collections.Specialized;
using ApexaAdvisors.Application.Models;
using ApexaAdvisors.Domain.Contracts;
using ApexaAdvisors.Domain.Events;
using ApexaAdvisors.Domain.Models.Dtos;
using MediatR;

namespace ApexaAdvisors.Application.DomainEventHandlers;

public class AdvisorCreatedOrUpdatedDomainEventHandler : INotificationHandler<AdvisorCreatedOrUpdatedDomainEvent>
{
    private readonly IAdvisorCacheService _advisorCacheService;

    public AdvisorCreatedOrUpdatedDomainEventHandler(IAdvisorCacheService advisorCacheService)
    {
        _advisorCacheService = advisorCacheService;
    }
    public Task Handle(AdvisorCreatedOrUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        _advisorCacheService.Update(AdvisorDetailsResponse.From(notification.Advisor));
        return Task.CompletedTask;
    }
}
