using ApexaAdvisors.Application.Models;
using ApexaAdvisors.Domain.Contracts;
using ApexaAdvisors.Domain.Events;
using ApexaAdvisors.Domain.Resources;
using MediatR;

namespace ApexaAdvisors.Application.Commands;

public record DeleteAdvisorCommand(Guid Id) : IRequest<Result<AdvisorDeletedResponse>>;

public class DeleteAdvisorCommandHandler : IRequestHandler<DeleteAdvisorCommand, Result<AdvisorDeletedResponse>>
{
    private readonly IAdvisorCommandRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;

    public DeleteAdvisorCommandHandler(IAdvisorCommandRepository repository, IUnitOfWork unitOfWork, IMediator mediator)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }

    public async Task<Result<AdvisorDeletedResponse>> Handle(
        DeleteAdvisorCommand request, CancellationToken cancellationToken)
    {
        var advisor = await _repository.GetById(request.Id);

        if (advisor is null)
            return Result<AdvisorDeletedResponse>.Error(nameof(CommonResource.Validations_AdvisorNotFound));

        _repository.Delete(advisor);
        await _unitOfWork.Commit();

        await _mediator.Publish(new AdvisorDeletedDomainEvent(advisor), cancellationToken);

        return Result<AdvisorDeletedResponse>.Success(new AdvisorDeletedResponse
        {
            Id = request.Id
        });
    }
}
