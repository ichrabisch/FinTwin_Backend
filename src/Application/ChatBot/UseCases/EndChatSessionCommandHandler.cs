using Application.ChatBot.Commands;
using FluentResults;
using FluentValidation;
using SharedKernel.Abstractions;


namespace Application.ChatBot.UseCases;

internal class EndChatSessionCommandHandler : ICommandHandler<EndChatSessionCommand, Result>
{
    private readonly IChatSessionRepository _chatSessionRepository;

    public EndChatSessionCommandHandler(
        IChatSessionRepository chatSessionRepository
        )
    {
        _chatSessionRepository = chatSessionRepository;
    }

    public async Task<Result> Handle(EndChatSessionCommand command, CancellationToken cancellationToken)
    {
        var chatSession = await _chatSessionRepository.GetByIdAsync(command.SessionId, cancellationToken);
        if (chatSession == null)
        {
            return Result.Fail($"Chat session with id {command.SessionId} not found.");
        }

        chatSession.EndSession();
        await _chatSessionRepository.UpdateAsync(chatSession, cancellationToken);

        return Result.Ok();
    }
}
