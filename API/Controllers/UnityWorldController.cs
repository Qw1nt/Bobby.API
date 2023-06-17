using Application.Common.Interfaces;
using Application.UnityWorlds.Commands.MockSave;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

/// <summary>
/// 
/// </summary>
[Route("unity-world")]
public class UnityWorldController : ApiControllerBase
{
    private readonly IApplicationDataContext _applicationDataContext;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    public UnityWorldController(ISender sender, IApplicationDataContext applicationDataContext) : base(sender)
    {
        _applicationDataContext = applicationDataContext;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var result = await _applicationDataContext.GameWorlds
            .Include(x => x.Objects)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        return Ok(result);
    }

    /// <summary>
    /// Сохранить мир
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> MockSaveWorld([FromBody] UnityWorldSaveCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }
}