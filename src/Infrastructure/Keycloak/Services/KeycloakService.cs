using Keycloak.AuthServices.Sdk;
using Keycloak.AuthServices.Sdk.Admin;
using Users.Domain.Aggregates.UserAggregate.Entities;
using Keycloak.AuthServices.Sdk.Admin.Requests.Users;
using Users.Domain.ValueObjects;
using Users.Infrastructure.Keycloak.Mappers;
using Users.Application.Common.Services;
using Keycloak.AuthServices.Sdk.Admin.Models;

namespace Users.Infrastructure.Keycloak.Services;

public class KeycloakService : IKeycloakService
{
    public const string ServiceName = nameof(KeycloakService);
    private readonly IKeycloakClient _keycloakClient;
    private readonly KeycloakAdminClientOptions _adminClientOptions;

    public KeycloakService(KeycloakAdminClientOptions adminClientOptions, IKeycloakClient keycloakClient)
    {
        _adminClientOptions = adminClientOptions;
        _keycloakClient = keycloakClient;
    }

    public async Task CreateUserAsync(User user, string password, CancellationToken cancellationToken)
    {
        if (await UserExists(user.Email))
            throw new InvalidOperationException("Email is already being used.");

        var keycloakUser = user.ToUserRepresentation(password);

        var response = await _keycloakClient.CreateUserWithResponseAsync(
                                            realm: _adminClientOptions.Realm,
                                            user: keycloakUser,
                                            cancellationToken: cancellationToken);

        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateUserAsync(User user, CancellationToken cancellationToken)
    {
        var keycloakUser = await GetKeycloakUserByEmail(user.Email);

        var response = await _keycloakClient.UpdateUserWithResponseAsync(
                                            realm: _adminClientOptions.Realm,
                                            userId: keycloakUser.Id!,
                                            user: keycloakUser,
                                            cancellationToken: cancellationToken);

        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteUserAsync(User user, CancellationToken cancellationToken)
    {
        var keycloakUser = await GetKeycloakUserByEmail(user.Email);

        var response = await _keycloakClient.DeleteUserWithResponseAsync(
                                            realm: _adminClientOptions.Realm,
                                            userId: keycloakUser.Id!,
                                            cancellationToken: cancellationToken);

        response.EnsureSuccessStatusCode();
    }

    public async Task<bool> UserExists(Email email)
    {
        var users = await _keycloakClient.GetUsersAsync(
                                        realm: _adminClientOptions.Realm,
                                        parameters: new GetUsersRequestParameters { Email = email },
                                        cancellationToken: CancellationToken.None);

        return users.Count() > 0;
    }

    private async Task<UserRepresentation> GetKeycloakUserByEmail(Email email)
    {
        var users = await _keycloakClient.GetUsersAsync(
                                                realm: _adminClientOptions.Realm,
                                                parameters: new GetUsersRequestParameters { Email = email },
                                                cancellationToken: CancellationToken.None);

        return users.FirstOrDefault() ?? throw new InvalidOperationException("User not found.");
    }
}
