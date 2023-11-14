using Domain.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace MarketPlace.IdentityServer.Extensions;

internal static class ServiceCollectionExtensions
{
    private static readonly string Policy = "any";
    
    internal static void AddIdentityCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(Policy,
                policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });
    }

    internal static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddRoleManager<RoleManager<IdentityRole<Guid>>>()
            .AddUserManager<UserManager<User>>()
            .AddDefaultTokenProviders();
    }
    internal static void ConfigureIdentityServerContexts(this IServiceCollection services, IConfiguration configuration)
    {
       services.AddIdentityServer(options =>
           {
               options.UserInteraction.LoginUrl = null;
           })
           .AddConfigurationStore(options =>
           {
               options.ConfigureDbContext = context =>
                   context.UseNpgsql(configuration.GetConnectionString("configurationDb"),
                       migration => migration.MigrationsAssembly(typeof(Program).Assembly.GetName().Name));
           })
           .AddOperationalStore(options =>
           {
               options.ConfigureDbContext = context =>
                   context.UseNpgsql(configuration.GetConnectionString("operationalDb"),
                       migration => migration.MigrationsAssembly(typeof(Program).Assembly.GetName().Name));
           })
           .AddDeveloperSigningCredential()
           .AddAspNetIdentity<User>();
    }

    internal static void UseCorsWithPolicy(this IApplicationBuilder app)
    {
        app.UseCors(Policy);
    }
}