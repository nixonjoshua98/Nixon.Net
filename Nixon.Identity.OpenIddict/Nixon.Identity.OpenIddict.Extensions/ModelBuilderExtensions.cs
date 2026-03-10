using Microsoft.EntityFrameworkCore;
using OpenIddict.EntityFrameworkCore.Models;

namespace Nixon.Identity.OpenIddict.Extensions;

public static class ModelBuilderExtensions
{
    public static ModelBuilder UseOpenIddict(this ModelBuilder modelBuilder, string schemaName)
    {
        modelBuilder.UseOpenIddict();
        
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            for (var type = entity; type != null; type = type.BaseType)
            {
                if (type.ClrType.Assembly != typeof(OpenIddictEntityFrameworkCoreApplication).Assembly) continue;
                
                entity.SetSchema(schemaName);

                break;
            }
        }
        
        return modelBuilder;
    }
}