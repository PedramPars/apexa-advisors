using ApexaAdvisors.Application.Commands;
using ApexaAdvisors.Application.Queries;
using ApexaAdvisors.Domain.Models.Dtos;
using ApexaAdvisors.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApexaAdvisors.Controllers;

[Route("api/advisors")]
public class AdvisorController : Controller
{
    private readonly IMediator _mediator;

    public AdvisorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAdvisors([FromQuery] AdvisorSearchPrameters searchParameters)
    {
        var result = await _mediator.Send(new GetAdvisorsQuery(searchParameters));
        return result.ToApiResult();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAdvisor(Guid id)
    {
        var result = await _mediator.Send(new GetAdvisorDetailsQuery(id));
        return result.ToApiResult();
    }

    [HttpPost]
    public async Task<IActionResult> AddAdvisor([FromBody]AddAdvisorCommand request)
    {
        var result = await _mediator.Send(request);
        return result.ToApiResult();
    }

    [HttpPut]
    public async Task<IActionResult> AddAdvisor([FromBody]UpdateAdvisorCommand request)
    {
        var result = await _mediator.Send(request);
        return result.ToApiResult();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> AddAdvisor(Guid id)
    {
        var result = await _mediator.Send(new DeleteAdvisorCommand(id));
        return result.ToApiResult();
    }
}
