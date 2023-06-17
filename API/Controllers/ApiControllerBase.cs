using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// 
/// </summary>
[ApiController]
public class ApiControllerBase : ControllerBase
{
    private readonly ISender _sender;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    public ApiControllerBase(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// 
    /// </summary>
    public ISender Mediator => _sender;
}