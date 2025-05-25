using Domain;
using Domain.Members.Model;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using WebAPI.Options;
using WebAPI.V1.Members.Endpoints;

namespace WebAPI;

internal static class Configurations
{
    public static IServiceCollection AppConfiguration(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddAutoMapperProfilesFromApplicationLayer(typeof(MemberProfile).Assembly);

        services.ConfigureOptions<JwtOptionsSetup>();
        services.ConfigureOptions<JwtBearerOptionsSetup>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();
        
        services.AddMemberDependencies();
       
        services.DomainLayerDependencies();
        services.ApplicationLayerDependencies();
        services.InfraLayerDependencies();
        return services;
    }

    
}
