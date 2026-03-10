using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Nixon.Identity.OpenIddict.Template.Configuration;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
public class OpenIddictFrameworkConfiguration : 
    OpenIddictFrameworkConfiguration<OpenIddictFrameworkApplicationConfiguration>
{
    
}

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
[SuppressMessage("ReSharper", "CoVariantArrayConversion")]
public class OpenIddictFrameworkConfiguration<TApplication> : IOpenIddictFrameworkConfiguration
    where TApplication : OpenIddictFrameworkApplicationConfiguration
{
    public string Issuer { get; init; } = null!;
    public string EncryptionKey { get; init; } = null!;
    
    public string ClientId { get; init; } = null!;
    public string ClientSecret { get; init; } = null!;

    public IEnumerable<string> AllAllowedGrantTypes
        => Applications.SelectMany(x => x.AllowedGrantTypes).Distinct();

    public TApplication[] Applications { get; init; } = [];

    IOpenIddictFrameworkApplicationConfiguration[] IOpenIddictFrameworkConfiguration.Applications => Applications;

    public SecurityKey EncryptionSecurityKey => 
        field ??= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(EncryptionKey));

    public virtual IEnumerable<string> GetRedirectUris(IOpenIddictFrameworkApplicationConfiguration application)
    {
        return application.GetRedirectUris();
    }
}