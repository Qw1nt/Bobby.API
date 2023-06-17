using Application.UnityGameObjects.Commands;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// 
/// </summary>
[Route("unity-game-object")]
public class UnityGameObjectController : ApiControllerBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    public UnityGameObjectController(ISender sender) : base(sender)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddUnityGameObjectCommand command)
    {
        var result = await Mediator.Send(command);
        return result == true ? Ok() : BadRequest();
    }
}