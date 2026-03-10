namespace Nixon.Identity.OpenIddict.Template.Configuration;

public interface IOpenIddictFrameworkApplicationConfiguration
{
    string ClientId { get;  } 
    
    string[] AllowedGrantTypes { get;  }

    IEnumerable<string> GetRedirectUris();
}