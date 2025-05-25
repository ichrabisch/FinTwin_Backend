using FluentResults;
using SharedKernel.Abstractions;
using Domain.ChatBot.ValueObjects;
using System;

namespace Application.ChatBot.Commands;

public sealed record DeleteChatMessageCommand(
    Guid MessageId
) : ICommand<Result>;
