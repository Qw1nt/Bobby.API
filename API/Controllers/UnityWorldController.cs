using Application.Common.Interfaces;
using Application.UnityWorlds.Commands.AddExploitationTime;
using Application.UnityWorlds.Commands.AddExtractedResourcesCost;
using Application.UnityWorlds.Commands.MockSave;
using Irony.Ast;
using Mediator;
using Microsoft.AspNetCore.Authorization;
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

    /// <summary>
    /// Получить все миры
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetActive()
    {
        var result = await _applicationDataContext.UnityWorlds
            .AsNoTracking()
            .Where(x => x.Active == true)
            .ToListAsync();
        
        return Ok(result);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var result = await _applicationDataContext.UnityWorlds
            .AsNoTracking()
            .Include(x => x.Objects)
            .ThenInclude(x => x.UnityReference)
            .FirstOrDefaultAsync(x => x.Id == id);

        return Ok(result);
    }

    /// <summary>
    /// Добавить эксплуатационное время
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut("exploitation-time/add")]
    public async Task<IActionResult> AddExploitationTime([FromBody] AddExploitationTimeCommand command)
    {
        var result = await Mediator.Send(command);
        return result == true ? Ok() : BadRequest();
    }

    /// <summary>
    /// Добавить ценность извл. ресурсов
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut("resources/cost/add")]
    public async Task<IActionResult> AddExtractedResourcesCost([FromBody] AddExtractedResourcesCostCommand command)
    {
        var result = await Mediator.Send(command);
        return result == true ? Ok() : BadRequest();
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

    /// <summary>
    /// Удалить объект
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // [Authorize]
    [HttpPut("object/{id:int}")]
    public async Task<IActionResult> RemoveObject([FromRoute] int id)
    {
        var gameObject = await _applicationDataContext.UnityWorldGameObjects
            .FirstOrDefaultAsync(x => x.Id == id);

        if (gameObject == null)
            return BadRequest();
        
        _applicationDataContext.UnityWorldGameObjects.Remove(gameObject);
        // _applicationDataContext.Users.
        
        await _applicationDataContext.SaveChangesAsync();
        return Ok();
    }
    
    /// <summary>
    /// Удалить игровой мир
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> RemoveWorld([FromRoute] int id)
    {
        await using var transaction = await _applicationDataContext.Database.BeginTransactionAsync();

        try
        {
            var world = await _applicationDataContext.UnityWorlds
                .Include(x => x.Objects)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (world == null)
                return BadRequest();

            _applicationDataContext.UnityWorlds.Remove(world);
            await _applicationDataContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch(Exception exception)
        {
            await transaction.RollbackAsync();
        }

        return Ok();
    }
}