using MediatR;
using Users.Application.Common.Services;
using Users.Domain.Aggregates.UserAggregate;
using Users.Domain.Common;

namespace Users.Application.UseCases.Users.Commands.DeleteUser;

public class DeleteUserHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IKeycloakService _keycloakService;

    public DeleteUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IKeycloakService keycloakService)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _keycloakService = keycloakService;
    }

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken) ??
             throw new KeyNotFoundException("The user was not found");

        await _userRepository.DeleteAsync(request.Id, cancellationToken);
        await _keycloakService.DeleteUserAsync(user, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);
    }
}
