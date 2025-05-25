using Application.Common.Abstractions;
using Domain.Accounts.Repository;
using Domain.Members.Repository;
using Infrastructure.Authentication;
using Infrastructure.Data.Accounts;
using Infrastructure.Data.ChatBot;
using Infrastructure.Data.Members;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Abstractions;

namespace Infrastructure;

public static class InfraLayerConfig
{
    public static void InfraLayerDependencies(this IServiceCollection services)
    {
        var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

        var infrastructureSettingsPath = Path.GetFullPath(Path.Combine(currentDirectory, "..", "..", "..", "..", "Infrastructure", "InfrastructureSettings.json"));

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Path.GetDirectoryName(infrastructureSettingsPath)!)
            .AddJsonFile(Path.GetFileName(infrastructureSettingsPath))
            .Build();

        services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString),
                mySqlOptions => mySqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null)
            );
        });

        services.AddScoped<IDbOperations, DbOperations>();

        services.AddScoped<IDbOperations, DbOperations>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IAccountReadRepository, AccountReadRepository>();
        services.AddScoped<IAccountWriteRepository, AccountWriteRepository>();
        services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
        services.AddScoped<IChatSessionRepository, ChatSessionRepository>();
        services.AddScoped<IJwtProvider, JwtProvider>();

    }
}
