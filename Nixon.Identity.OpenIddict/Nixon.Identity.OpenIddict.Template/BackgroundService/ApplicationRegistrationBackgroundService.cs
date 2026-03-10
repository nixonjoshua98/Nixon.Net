using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nixon.Identity.OpenIddict.Extensions;
using Nixon.Identity.OpenIddict.Template.Configuration;
using OpenIddict.Abstractions;

namespace Nixon.Identity.OpenIddict.Template.BackgroundService;

    internal sealed class ApplicationRegistrationBackgroundService(
        IOpenIddictFrameworkConfiguration configuration,
        IServiceProvider serviceProvider
    ) : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await using var scope = serviceProvider.CreateAsyncScope();

            var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

            foreach (var application in configuration.Applications)
            {
                var descriptor = CreateApplicationDescriptor(application);
                
                await manager.CreateOrUpdateAsync(descriptor, cancellationToken);
            }
        }

        private OpenIddictApplicationDescriptor CreateApplicationDescriptor(
            IOpenIddictFrameworkApplicationConfiguration application
        )
        {
            var descriptor = new OpenIddictApplicationDescriptor
            {
                ClientId = application.ClientId,
                ClientType = OpenIddictConstants.ClientTypes.Public,
                Permissions =
                {
                    OpenIddictConstants.Permissions.Endpoints.Token,
                    OpenIddictConstants.Permissions.Endpoints.Authorization,

                    OpenIddictConstants.Permissions.ResponseTypes.Code,

                    OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                    OpenIddictConstants.Permissions.GrantTypes.RefreshToken
                }
            };

            descriptor.AddGrantTypePermissions(application.AllowedGrantTypes);
            descriptor.AddRedirectUris(configuration.GetRedirectUris(application));

            return descriptor;
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }