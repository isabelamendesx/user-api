using MediatR;
using Users.Domain.Aggregates.UserAggregate;
using Users.Domain.Common;

namespace Users.Application.UseCases.Users.Commands.DeleteUser;

public class DeleteUserHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        await _userRepository.DeleteAsync(request.Id, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}
