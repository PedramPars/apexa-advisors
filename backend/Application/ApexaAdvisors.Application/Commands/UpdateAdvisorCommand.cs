using System.ComponentModel.DataAnnotations;
using ApexaAdvisors.Application.Models;
using ApexaAdvisors.Domain.Contracts;
using ApexaAdvisors.Domain.Models.Entities;
using ApexaAdvisors.Domain.Models.ValueObjects;
using ApexaAdvisors.Domain.Resources;
using MediatR;

namespace ApexaAdvisors.Application.Commands;

public record UpdateAdvisorCommand : IRequest<Result<AdvisorUpdatedResponse>>
{
    [Required]
    public Guid Id { get; set; }

    [Required(ErrorMessage=nameof(CommonResource.Validations_NameIsRequired))]
    public required string Name { get; set; }

    [Required(ErrorMessage=nameof(CommonResource.Validations_SinIsRequired))]
    public required string Sin { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }
}

public class UpdateAdvisorCommandHandler : IRequestHandler<UpdateAdvisorCommand, Result<AdvisorUpdatedResponse>>
{
    private readonly IAdvisorCommandRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAdvisorCommandHandler(IAdvisorCommandRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<AdvisorUpdatedResponse>> Handle(
        UpdateAdvisorCommand request, CancellationToken cancellationToken)
    {
        var validations = Advisor.Validate(request.Name, request.Sin)
            .Concat(ContactInformation.Validate(request.Address, request.Phone))
            .ToList();

        var existingAdvisor = await _repository.GetBySin(request.Sin);
        if (existingAdvisor is not null && existingAdvisor.Id != request.Id)
            validations.Add(nameof(CommonResource.validations_SinAlreadyExists));

        if (validations.Any())
            return Result<AdvisorUpdatedResponse>.Error(validations);

        var advisor = await _repository.GetById(request.Id);

        if (advisor is null)
            return Result<AdvisorUpdatedResponse>.Error(nameof(CommonResource.Validations_AdvisorNotFound));

        var contactInformation = new ContactInformation(request.Address, request.Phone);

        advisor.SetName(request.Name);
        advisor.SetSin(request.Sin);
        advisor.SetContactInformation(contactInformation);


        _repository.Update(advisor);
        await _unitOfWork.Commit();

        return Result<AdvisorUpdatedResponse>.Success(new AdvisorUpdatedResponse
        {
            Id = request.Id
        });
    }
}
