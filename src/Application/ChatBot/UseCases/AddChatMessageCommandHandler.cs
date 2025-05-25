using Application.ChatBot.Commands.AddChatMessageCommand;
using AutoMapper;
using Domain.ChatBot.Model;
using FluentResults;
using FluentValidation;
using SharedKernel.Abstractions;


namespace Application.ChatBot.UseCases;

internal class AddChatMessageCommandHandler : ICommandHandler<AddChatMessageCommand, Result<Guid>>
{
    private readonly IChatMessageRepository _chatMessageRepository;
    private readonly IChatSessionRepository _chatSessionRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<AddChatMessageCommand> _validator;

    public AddChatMessageCommandHandler(
        IChatMessageRepository chatMessageRepository,
        IChatSessionRepository chatSessionRepository,
        IMapper mapper,
        IValidator<AddChatMessageCommand> validator)
    {
        _chatMessageRepository = chatMessageRepository;
        _chatSessionRepository = chatSessionRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Result<Guid>> Handle(AddChatMessageCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Fail(validationResult.Errors.Select(e => e.ErrorMessage));
        }

        var sessionExists = await _chatSessionRepository.ExistsAsync(command.SessionId, cancellationToken);
        if (!sessionExists)
        {
            return Result.Fail($"Chat session with id {command.SessionId} not found.");
        }

        var chatMessage = _mapper.Map<ChatMessage>(command);
        await _chatMessageRepository.AddAsync(chatMessage, cancellationToken);

        return Result.Ok(chatMessage.Id);
    }
}