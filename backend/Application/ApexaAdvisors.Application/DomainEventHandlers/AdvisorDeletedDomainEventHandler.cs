using ApexaAdvisors.Domain.Contracts;
using ApexaAdvisors.Domain.Events;
using ApexaAdvisors.Domain.Models.Dtos;
using MediatR;

namespace ApexaAdvisors.Application.DomainEventHandlers;

public class AdvisorDeletedDomainEventHandler : INotificationHandler<AdvisorDeletedDomainEvent>
{
    private readonly IAdvisorCacheService _advisorCacheService;

    public AdvisorDeletedDomainEventHandler(IAdvisorCacheService advisorCacheService)
    {
        _advisorCacheService = advisorCacheService;
    }
    public Task Handle(AdvisorDeletedDomainEvent notification, CancellationToken cancellationToken)
    {
        _advisorCacheService.Delete(AdvisorDetailsResponse.From(notification.Advisor));
        return Task.CompletedTask;
    }
}
