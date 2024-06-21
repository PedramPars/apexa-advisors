using System.ComponentModel.DataAnnotations;
using ApexaAdvisors.Application.Models;
using ApexaAdvisors.Application.Services;
using ApexaAdvisors.Domain.Contracts;
using ApexaAdvisors.Domain.Models.Entities;
using ApexaAdvisors.Domain.Models.ValueObjects;
using ApexaAdvisors.Domain.Resources;
using MediatR;

namespace ApexaAdvisors.Application.Commands;

public record AddAdvisorCommand : IRequest<Result<AdvisorCreatedResponse>>
{
    [Required(ErrorMessage=nameof(CommonResource.Validations_NameIsRequired))]
    public required string Name { get; set; }

    [Required(ErrorMessage=nameof(CommonResource.Validations_SinIsRequired))]
    public required string Sin { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }
}

public class AddAdvisorCommandHandler : IRequestHandler<AddAdvisorCommand, Result<AdvisorCreatedResponse>>
{
    private readonly IAdvisorCommandRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly HealthGenerator _healthGenerator;

    public AddAdvisorCommandHandler(
        IAdvisorCommandRepository repository, IUnitOfWork unitOfWork, HealthGenerator healthGenerator)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _healthGenerator = healthGenerator;
    }

    public async Task<Result<AdvisorCreatedResponse>> Handle(AddAdvisorCommand request, CancellationToken cancellationToken)
    {
        var validations = Advisor.Validate(request.Name, request.Sin)
            .Concat(ContactInformation.Validate(request.Address, request.Phone))
            .ToList();

        var existingAdvisor = await _repository.GetBySin(request.Sin);
        if (existingAdvisor is not null)
            validations.Add(nameof(CommonResource.validations_SinAlreadyExists));

        if (validations.Any())
            return Result<AdvisorCreatedResponse>.Error(validations);

        var contactInformation = new ContactInformation(request.Address, request.Phone);

        var id = Guid.NewGuid();
        var advisor = new Advisor(id, request.Name, request.Sin, contactInformation);
        advisor.SetHealthStatus(_healthGenerator.Generate());

        await _repository.Create(advisor);
        await _unitOfWork.Commit();

        return Result<AdvisorCreatedResponse>.Success(new AdvisorCreatedResponse
        {
            Id = id
        });
    }
}
