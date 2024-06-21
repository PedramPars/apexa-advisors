using ApexaAdvisors.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApexaAdvisors.Helpers;

public static class Helpers
{
    public static IActionResult ToApiResult<T>(this Result<T> result)
    {
        if (!result.IsError && !result.Errors.Any())
            return new OkObjectResult(result);

        return new BadRequestObjectResult(result);
    }
}
