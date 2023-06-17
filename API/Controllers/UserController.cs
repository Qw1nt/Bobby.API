using System.Diagnostics;
using Application.Users.Queries;
using Infrastructure.Extensions;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// 
/// </summary>
[Route("user")]
public class UserController : ApiControllerBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    public UserController(ISender sender) : base(sender)
    {
    }

    // TODO Add id
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet("unit/characteristics")]
    public async Task<IActionResult> GetUnitCharacteristics()
    {
        if (this.TryGetIdClaim(out var id) == false)
            return Unauthorized();
        
        var result = await Mediator.Send(new GetUnitCharacteristicQuery(id));
        return result == null ? BadRequest() : Ok(result);
    }
}