using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Nixon.Identity.OpenIddict.Extensions;
using Nixon.Identity.OpenIddict.Template.Configuration;

namespace Nixon.Identity.OpenIddict.Template.Builders;

public sealed class OpenIddictFrameworkClientBuilder(IOpenIddictFrameworkConfiguration configuration, IHostEnvironment environment)
{
    private Action<OpenIddictClientBuilder>? _configureAction;
    
    private bool _hasSigningKey;

    internal void Configure(OpenIddictClientBuilder builder)
    {
        if (!_hasSigningKey)
        {
            builder.AddDevelopmentSigningCertificate();
        }
        
        builder.SetRedirectionEndpointUris("connect/redirect");

        builder.AllowAuthorizationCodeFlow();

        builder.UseDataProtection();
        builder.UseSystemNetHttp();

        builder.AddValidateIssuerGoogleWorkaround();
                
        builder.AddEncryptionKey(configuration.EncryptionSecurityKey);
                
        builder.UseAspNetCore(asp => asp
            .EnableRedirectionEndpointPassthrough()
            .DisableDevelopmentTransportSecurityRequirement(environment)
        );
        
        _configureAction?.Invoke(builder);   
    }
    
    public OpenIddictFrameworkClientBuilder UseWebProviders(Action<OpenIddictClientWebIntegrationBuilder> action)
    {
        _configureAction += builder =>
            builder.UseWebProviders(action);
        
        return this;
    }
    
    public OpenIddictFrameworkClientBuilder AddSigningKey(SecurityKey key)
    {
        _configureAction += builder => builder
            .AddSigningKey(key);

        _hasSigningKey = true;
        
        return this;
    }
}