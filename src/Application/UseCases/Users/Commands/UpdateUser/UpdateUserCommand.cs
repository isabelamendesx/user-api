using MediatR;
using System.Text.Json.Serialization;

namespace Users.Application.UseCases.Users.Commands.UpdateUser;

public sealed record UpdateUserCommand : IRequest<UpdateUserResponse>
{
    [JsonIgnore]
    public Guid UserId { get; set; }
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string IDD { get; init; } = null!;
    public string Phone { get; init; } = null!;
}
