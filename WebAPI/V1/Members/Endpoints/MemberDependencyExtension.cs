using Application.Members.Commands;
using Domain.Members.Model;
using Domain.Members.Repository;
using FluentValidation;
using Infrastructure.Data.Members;

namespace WebAPI.V1.Members.Endpoints;

internal static class MemberDependencyExtension
{
    public static IServiceCollection AddMemberDependencies(this IServiceCollection services)
    {
        return services;
    }
}