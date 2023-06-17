using Application.Common.Interfaces;
using Application.Identity.Commands.Authorized;
using Application.Identity.Commands.Register;
using Domain.Common;
using Domain.Entities;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

/// <summary>
/// Авторизация
/// </summary>
[Route("identity")]
public class IdentityController : ApiControllerBase
{
    private readonly IApplicationDataContext _applicationDataContext;

    /// <summary>
    /// Конструктор класса
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="applicationDataContext"></param>
    public IdentityController(ISender sender, IApplicationDataContext applicationDataContext) : base(sender)
    {
        _applicationDataContext = applicationDataContext;
    }

    /// <summary>
    /// Зарегистрировать пользователя
    /// </summary>
    /// <returns></returns>
    [HttpPost("register")]
    [ProducesResponseType(typeof(IdentityKeyValuePair), StatusCodes.Status200OK)]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        var alreadyExist = await _applicationDataContext.Users
            .AsNoTracking()
            .AnyAsync(x => x.Email == command.Email);

        if (alreadyExist == true)
            return BadRequest("Пользователь с таким именем уже зарегистрирован");

        var response = await Mediator.Send(command);
        return Ok(response);
    }

    /// <summary>
    /// Авторизовать пользователя
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(IdentityKeyValuePair), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login([FromBody] AuthenticateCommand command)
    {
        var response = await Mediator.Send(command);
        return Ok(response);
    }
}