using MediatR;
using Microsoft.AspNetCore.Mvc;
using Users.Application.UseCases.Commands.CreateUser;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly ISender _sender;

    public UsersController(ILogger<UsersController> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
    {
        var response = await _sender.Send(command, cancellationToken);

        return StatusCode(StatusCodes.Status201Created, response);
    }
}
