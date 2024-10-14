using Duende.AccessTokenManagement;
using Keycloak.AuthServices.Common;
using Keycloak.AuthServices.Sdk;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Users.Application.Common.Services;
using Users.Infrastructure.Keycloak.Services;

namespace Users.Infrastructure.Keycloak;

public static class DependencyInjection
{
    public static IServiceCollection AddKeycloak(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IKeycloakService, KeycloakService>();

        var managementOptions = configuration.GetKeycloakOptions<KeycloakAdminClientOptions>("Keycloak:Management")!;

        // Configura o gerenciamento de tokens do tipo Client Credentials com cache de memória.
        // 1. Adiciona cache distribuído em memória para armazenar tokens.
        // 2. Adiciona o gerenciamento automático de tokens de credenciais de cliente.
        // 3. Define um cliente com as credenciais necessárias para obter tokens do Keycloak.
        services
            .AddDistributedMemoryCache()
            .AddClientCredentialsTokenManagement()
            .AddClient(KeycloakService.ServiceName, client => SetClientCredentials(client, managementOptions));

        // Configura um HttpClient para se comunicar com a Admin API do Keycloak.
        // 1. Cria um cliente HTTP configurado para acessar a API do Keycloak, utilizando as opções fornecidas.
        // 2. Adiciona um handler que gerencia automaticamente os tokens de Client Credentials
        services
            .AddKeycloakAdminHttpClient(managementOptions)
            .AddClientCredentialsTokenHandler(KeycloakService.ServiceName);

        services.AddSingleton(managementOptions);

        return services;
    }

    private static void SetClientCredentials(ClientCredentialsClient client, KeycloakInstallationOptions options)
    {
        client.ClientId = options.Resource;
        client.ClientSecret = options.Credentials.Secret;
        client.TokenEndpoint = options.KeycloakTokenEndpoint;
    }
}
