using MediatR;

namespace Users.Application.UseCases.Users.Queries.GetUserById;

public sealed record class GetUserByIdQuery : IRequest<GetUserByIdResponse>
{
    public Guid Id { get; init; }
}
