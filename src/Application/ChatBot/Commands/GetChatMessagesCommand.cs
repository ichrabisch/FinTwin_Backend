using Application.ChatBot.Commands.AddChatMessageCommand;
using FluentResults;
using SharedKernel.Abstractions;

namespace Application.ChatBot.Commands;


public sealed record GetChatMessagesCommand(Guid SessionId) : ICommand<Result<List<ChatMessageDto>>>;