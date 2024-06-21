using ApexaAdvisors.Application.Models;
using ApexaAdvisors.Domain.Contracts;
using ApexaAdvisors.Domain.Models.Dtos;
using ApexaAdvisors.Domain.Models.Enums;
using ApexaAdvisors.Domain.Resources;
using MediatR;

namespace ApexaAdvisors.Application.Queries;

public record GetAdvisorDetailsQuery(Guid Id) : IRequest<Result<AdvisorDetailsResponse>>;

public class GetAdvisorDetailsQueryHandler : IRequestHandler<GetAdvisorDetailsQuery, Result<AdvisorDetailsResponse>>
{
    private readonly IAdvisorQueryRepository _repository;
    private readonly IAdvisorCacheService _advisorCacheService;

    public GetAdvisorDetailsQueryHandler(IAdvisorQueryRepository repository, IAdvisorCacheService advisorCacheService)
    {
        _repository = repository;
        _advisorCacheService = advisorCacheService;
    }
    public async Task<Result<AdvisorDetailsResponse>> Handle(
        GetAdvisorDetailsQuery request, CancellationToken cancellationToken)
    {
        var cachedItem = _advisorCacheService.Get(request.Id);
        if (cachedItem is not null)
            return Result<AdvisorDetailsResponse>.Success(cachedItem);

        var result = await _repository.GetDetails(request.Id);
        if (result == null)
            return Result<AdvisorDetailsResponse>.Error(nameof(CommonResource.Validations_AdvisorNotFound));

        _advisorCacheService.Add(result);
        return Result<AdvisorDetailsResponse>.Success(result);
    }
}
