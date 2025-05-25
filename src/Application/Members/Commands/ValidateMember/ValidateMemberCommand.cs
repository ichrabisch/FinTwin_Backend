using FluentResults;
using SharedKernel.Abstractions;

namespace Application.Members.Commands.ValidateMember;

public sealed record ValidateMemberCommand(string Email, string Password) : ICommand<Result<string>>;
