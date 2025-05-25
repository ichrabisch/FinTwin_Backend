using Application.ChatBot.Commands.AddChatMessageCommand;
using FluentResults;
using SharedKernel.Abstractions;

namespace Application.ChatBot.Commands.StartChatSession;

public sealed record StartChatSessionCommand(
    Guid AccountId
) : ICommand<Result<Guid>>;

public record ChatSessionDto(
    Guid Id,
    Guid AccountId,
    DateTime StartTime,
    DateTime? EndTime,
    List<ChatMessageDto> Messages
);