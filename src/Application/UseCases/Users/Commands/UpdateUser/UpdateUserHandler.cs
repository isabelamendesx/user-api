using MediatR;
using Users.Domain.Aggregates.UserAggregate;
using Users.Domain.Common;
using Users.Domain.ValueObjects;

namespace Users.Application.UseCases.Users.Commands.UpdateUser;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UpdateUserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<UpdateUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken) ??
            throw new KeyNotFoundException("The user was not found");

        user.SetName(new Name(request.FirstName, request.LastName));
        user.SetPhone(new Phone(request.IDD, request.Phone));

        await _userRepository.UpdateAsync(user, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new UpdateUserResponse(user.Id);
    }
}
