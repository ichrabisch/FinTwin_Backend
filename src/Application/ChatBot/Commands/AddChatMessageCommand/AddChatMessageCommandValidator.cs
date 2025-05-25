using FluentValidation;

namespace Application.ChatBot.Commands.AddChatMessageCommand;

public class AddChatMessageCommandValidator : AbstractValidator<AddChatMessageCommand>
{
    public AddChatMessageCommandValidator()
    {
        RuleFor(x => x.SessionId).NotEmpty();
        RuleFor(x => x.Content).NotEmpty().MaximumLength(4096);
        RuleFor(x => x.SenderType).IsInEnum();
    }
}
