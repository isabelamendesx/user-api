using MediatR;
using Users.Domain.Aggregates.UserAggregate;
using Users.Domain.Aggregates.UserAggregate.Entities;
using Users.Domain.Common;
using Users.Domain.ValueObjects;

namespace Users.Application.UseCases.Users.Commands.CreateUser;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, CreateUserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateUserResponse> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        if (await _userRepository.EmailAlreadyExistsAsync(command.Email, cancellationToken))
            throw new Exception("Email is already being used.");

        var user = CreateUser(command);

        await _userRepository.AddAsync(user, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new CreateUserResponse(user.Id);
    }
    private static User CreateUser(CreateUserCommand command)
    {
        var name = new Name(command.FirstName, command.LastName);
        var email = new Email(command.Email);
        var phone = new Phone(command.IDD, command.Phone);
        var nickname = new Nickname(command.Nickname ?? command.FirstName);
        var user = new User(name, nickname, email, phone);

        return user;
    }
}
