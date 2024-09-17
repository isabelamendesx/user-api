using MediatR;
using Microsoft.AspNetCore.Mvc;
using Users.Application.UseCases.Users.Commands.CreateUser;
using Users.Application.UseCases.Users.Commands.DeleteUser;
using Users.Application.UseCases.Users.Commands.UpdateUser;
using Users.Application.UseCases.Users.Queries.GetUserById;
using Users.Application.UseCases.Users.Queries.GetUsers;

namespace WebApi.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly ISender _sender;

    public UsersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
    {
        var response = await _sender.Send(command, cancellationToken);

        return StatusCode(StatusCodes.Status201Created, response);
    }

    [HttpPut("{userId:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid userId, [FromBody] UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var response = await _sender.Send(command with { UserId = userId }, cancellationToken);

        return Ok(response);
    }

    [HttpDelete("{userId:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid userId, CancellationToken cancellationToken)
    {
        await _sender.Send(new DeleteUserCommand { Id = userId }, cancellationToken);

        return NoContent();
    }

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid userId, CancellationToken cancellationToken)
    {
        var response = await _sender.Send(new GetUserByIdQuery { Id = userId }, cancellationToken);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var query = new GetUsersQuery { PageNumber = pageNumber, PageSize = pageSize };
        var response = await _sender.Send(query, cancellationToken);

        return Ok(response);
    }
}
