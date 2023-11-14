using System.IdentityModel.Tokens.Jwt;
using Application.Interfaces;
using Domain.Common;
using Domain.Common.Interfaces;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration, string audience)
    {
        services.AddServices();
        services.ConfigureAuthentication(configuration, audience);
    }
    private static void AddServices(this IServiceCollection services)
    {
        services
            .AddTransient<IMediator, Mediator>()
            .AddTransient<IDomainEventDispatcher, DomainEventDispatcher>()
            .AddTransient<IDateTimeService, DateTimeService>()
            .AddTransient<IEmailService, EmailService>();
    }
    private static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration, string audience)
    {
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        var identityUrl = configuration.GetValue<string>("IdentityUrl");

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(options =>
        {
            options.Authority = identityUrl;
            options.RequireHttpsMetadata = false;
            options.Audience = audience;
            options.TokenValidationParameters.RoleClaimType = "role";
        });
    }
}