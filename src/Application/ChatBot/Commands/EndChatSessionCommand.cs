using FluentResults;
using SharedKernel.Abstractions;
namespace Application.ChatBot.Commands;

public sealed record EndChatSessionCommand(
    Guid SessionId
) : ICommand<Result>;

