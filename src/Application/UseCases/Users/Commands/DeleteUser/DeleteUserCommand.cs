using MediatR;

namespace Users.Application.UseCases.Users.Commands.DeleteUser;

public sealed record DeleteUserCommand : IRequest
{
    public Guid Id { get; init; }
}
