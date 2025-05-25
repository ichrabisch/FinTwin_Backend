using FluentResults;
using SharedKernel.Abstractions;
using Domain.ChatBot.ValueObjects;


namespace Application.ChatBot.Commands.AddChatMessageCommand;

public sealed record AddChatMessageCommand(
    Guid SessionId,
    string Content,
    SenderType SenderType
) : ICommand<Result<Guid>>;

public record ChatMessageDto(
    Guid Id,
    Guid SessionId,
    string Content,
    SenderType SenderType,
    DateTime Timestamp
);