using FluentResults;
using SharedKernel.Abstractions;

namespace Application.ChatBot.Commands;

public sealed record UpdateChatMessageCommand(
    Guid MessageId,
    string NewContent
) : ICommand<Result>;
