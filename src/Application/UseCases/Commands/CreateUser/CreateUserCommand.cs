using MediatR;

namespace Users.Application.UseCases.Commands.CreateUser;

public sealed record CreateUserCommand : IRequest<CreateUserResponse>
{
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string? Nickname { get; init; }
    public string Email { get; init; } = null!;
    public string IDD { get; init; } = null!;
    public string Phone { get; init; } = null!;
}
