using MediatR;
using Users.Domain.Aggregates.UserAggregate;
using Users.Domain.Aggregates.UserAggregate.Entities;

namespace Users.Application.UseCases.Users.Queries.GetUsers;

public class GetUsersHandler : IRequestHandler<GetUsersQuery, GetUsersResponse>  
{
    private readonly IUserRepository _userRepository;

    public GetUsersHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<GetUsersResponse> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.ListAsync(request.PageNumber, request.PageSize, cancellationToken);

        var hits = await _userRepository.CountAsync(cancellationToken);

        return new GetUsersResponse
        {
            Data = Map(users),
            Hits = hits
        };
    }

    private IEnumerable<GetUsersResponseData> Map(IEnumerable<User> users)
    {
        return users.Select(user => new GetUsersResponseData
        {
            Id = user.Id,
            Created = user.Created,
            Updated = user.Updated,
            FirstName = user.Name.FirstName,
            LastName = user.Name.LastName,
            Nickname = user.Nickname.Value,
            Email = user.Email.Address,
            Phone = user.Phone?.GetFullPhone(),
            IsActive = user.IsActive
        }).ToList();
    }
}
