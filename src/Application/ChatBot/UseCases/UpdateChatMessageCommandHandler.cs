using Application.ChatBot.Commands;
using FluentResults;
using SharedKernel.Abstractions;


namespace Application.ChatBot.UseCases
{
    internal class UpdateChatMessageCommandHandler : ICommandHandler<UpdateChatMessageCommand, Result>
    {
        private readonly IChatMessageRepository _chatMessageRepository;

        public UpdateChatMessageCommandHandler(
            IChatMessageRepository chatMessageRepository
            )
        {
            _chatMessageRepository = chatMessageRepository;
        }

        public async Task<Result> Handle(UpdateChatMessageCommand command, CancellationToken cancellationToken)
        {
            var chatMessage = await _chatMessageRepository.GetByIdAsync(command.MessageId, cancellationToken);
            if (chatMessage == null)
            {
                return Result.Fail($"Chat message with id {command.MessageId} not found.");
            }

            chatMessage.UpdateMessage(command.NewContent);
            await _chatMessageRepository.UpdateAsync(chatMessage, cancellationToken);

            return Result.Ok();
        }
    }
}
