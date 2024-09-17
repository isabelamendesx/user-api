using MediatR;

namespace Users.Application.UseCases.Users.Queries.GetUsers;

public sealed record GetUsersQuery : IRequest<GetUsersResponse>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 100;
}
