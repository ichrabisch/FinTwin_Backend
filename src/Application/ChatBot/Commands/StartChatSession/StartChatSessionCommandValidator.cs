using FluentValidation;
using Application.ChatBot.Commands;
using System;

namespace Application.ChatBot.Commands.StartChatSession;

public class StartChatSessionCommandValidator : AbstractValidator<StartChatSessionCommand>
{
    public StartChatSessionCommandValidator()
    {
        RuleFor(x => x.AccountId).NotEmpty();
    }
}