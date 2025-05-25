using Application.ChatBot.Commands;
using Application.ChatBot.Commands.AddChatMessageCommand;
using AutoMapper;
using FluentResults;
using SharedKernel.Abstractions;

namespace Application.ChatBot.UseCases;

public class GetChatMessagesCommandHandler : ICommandHandler<GetChatMessagesCommand, Result<List<ChatMessageDto>>>
{
    private readonly IChatMessageRepository _chatMessageRepository;
    private readonly IMapper _mapper;

    public GetChatMessagesCommandHandler(IChatMessageRepository chatMessageRepository, IMapper mapper)
    {
        _chatMessageRepository = chatMessageRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<ChatMessageDto>>> Handle(GetChatMessagesCommand command, CancellationToken cancellationToken)
    {
        var messages = await _chatMessageRepository.GetBySessionIdAsync(command.SessionId, cancellationToken);

        if (messages == null || !messages.Any())
        {
            return Result.Fail($"No messages found for session id {command.SessionId}");
        }

        var messageDtos = _mapper.Map<List<ChatMessageDto>>(messages);
        return Result.Ok(messageDtos);
    }
}
