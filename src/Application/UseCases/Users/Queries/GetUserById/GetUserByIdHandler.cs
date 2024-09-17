using MediatR;
using Users.Domain.Aggregates.UserAggregate;

namespace Users.Application.UseCases.Users.Queries.GetUserById;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdResponse>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<GetUserByIdResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken) 
            ?? throw new ArgumentException("User was not found.");

        return new GetUserByIdResponse
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
        };
    }
}
