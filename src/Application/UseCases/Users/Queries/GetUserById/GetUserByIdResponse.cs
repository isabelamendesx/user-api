namespace Users.Application.UseCases.Users.Queries.GetUserById;

public sealed record GetUserByIdResponse
{
    public Guid Id { get; init; }
    public DateTime Created { get; init; }
    public DateTime? Updated { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Nickname { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string? Phone { get; init; }
    public bool IsActive { get; init; }
}
