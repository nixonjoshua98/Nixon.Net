using System.Diagnostics.CodeAnalysis;

namespace Nixon.Identity.OpenIddict.Template.Configuration;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
public class OpenIddictFrameworkApplicationConfiguration : IOpenIddictFrameworkApplicationConfiguration
{
    public string ClientId { get; init; } = null!;
    
    public string[] AllowedGrantTypes { get; init; } = [];

    public virtual IEnumerable<string> GetRedirectUris()
    {
        return [];
    }
}