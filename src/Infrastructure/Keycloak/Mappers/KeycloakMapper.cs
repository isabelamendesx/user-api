using Keycloak.AuthServices.Sdk.Admin.Models;
using Users.Domain.Aggregates.UserAggregate.Entities;

namespace Users.Infrastructure.Keycloak.Mappers;

public static class KeycloakMapper
{
    public static UserRepresentation ToUserRepresentation(this User user, string password)
    {
        return new UserRepresentation
        {
            Email = user.Email.Address,
            FirstName = user.Name.FirstName,
            LastName = user.Name.LastName,
            Username = user.Email.Address,
            Enabled = true,
            EmailVerified = true,
            Credentials = [GetPasswordCredential(password)]
        };
    }

    private static CredentialRepresentation GetPasswordCredential(string password)
    {
        return new CredentialRepresentation
        {
            Type = "password",
            Temporary = false,
            Value = password
        };
    }
}
