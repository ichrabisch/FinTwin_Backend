using Application.ChatBot.Commands.StartChatSession;
using AutoMapper;
using Domain.ChatBot.Model;
using FluentResults;
using FluentValidation;
using SharedKernel.Abstractions;


namespace Application.ChatBot.UseCases;


internal class StartChatSessionCommandHandler : ICommandHandler<StartChatSessionCommand, Result<Guid>>
{
    private readonly IChatSessionRepository _chatSessionRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<StartChatSessionCommand> _validator;

    public StartChatSessionCommandHandler(
        IChatSessionRepository chatSessionRepository,
        IMapper mapper,
        IValidator<StartChatSessionCommand> validator)
    {
        _chatSessionRepository = chatSessionRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Result<Guid>> Handle(StartChatSessionCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Fail(validationResult.Errors.Select(e => e.ErrorMessage));
        }

        var chatSession = _mapper.Map<ChatSession>(command);
        await _chatSessionRepository.AddAsync(chatSession, cancellationToken);

        return Result.Ok(chatSession.Id);
    }
}
