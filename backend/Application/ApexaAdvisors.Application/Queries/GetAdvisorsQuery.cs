using ApexaAdvisors.Application.Models;
using ApexaAdvisors.Domain.Contracts;
using ApexaAdvisors.Domain.Models.Dtos;
using ApexaAdvisors.Domain.Models.Enums;
using MediatR;

namespace ApexaAdvisors.Application.Queries;

public record GetAdvisorsQuery(AdvisorSearchPrameters SearchParameters)
    : IRequest<Result<IEnumerable<AdvisorListResponse>>>
{
}

public class GetAdvisorsQueryHandler : IRequestHandler<GetAdvisorsQuery, Result<IEnumerable<AdvisorListResponse>>>
{
    private readonly IAdvisorQueryRepository _repository;

    public GetAdvisorsQueryHandler(IAdvisorQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<IEnumerable<AdvisorListResponse>>> Handle(
        GetAdvisorsQuery request, CancellationToken cancellationToken)
    {
        return Result<IEnumerable<AdvisorListResponse>>.Success(await _repository.GetList(request.SearchParameters));
    }
}
