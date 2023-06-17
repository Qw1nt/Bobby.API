using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// 
/// </summary>
[Route("unit")]
public class UnitController : ApiControllerBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    public UnitController(ISender sender) : base(sender)
    {
    }
}