using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;

namespace Nixon.Identity.OpenIddict.Template.Configuration;

public interface IOpenIddictFrameworkConfiguration
{
    string Issuer { get;  }
    
    string EncryptionKey { get;  }
    
    string ClientId { get;  }
    
    string ClientSecret { get;  }

    IEnumerable<string> AllAllowedGrantTypes  { get;  }

    IOpenIddictFrameworkApplicationConfiguration[] Applications { get;  } 

    SecurityKey EncryptionSecurityKey { get;  }

    IEnumerable<string> GetRedirectUris(IOpenIddictFrameworkApplicationConfiguration application);
}