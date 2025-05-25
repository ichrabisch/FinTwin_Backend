using FluentResults;
using SharedKernel.Abstractions;

namespace Application.ChatBot.Commands;

public sealed record ArchiveChatSessionCommand(
    Guid SessionId
) : ICommand<Result>;
