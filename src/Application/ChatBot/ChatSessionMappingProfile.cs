using Application.ChatBot.Commands;
using Application.ChatBot.Commands.AddChatMessageCommand;
using Application.ChatBot.Commands.StartChatSession;
using AutoMapper;
using Domain.ChatBot.Model;

namespace Application.ChatBot;

public class ChatSessionMappingProfile : Profile
{
    public ChatSessionMappingProfile()
    {
        CreateMap<StartChatSessionCommand, ChatSession>()
            .ConstructUsing((cmd, ctx) =>
            {
                return new ChatSession(
                    accountId: cmd.AccountId
                );
            });

        CreateMap<AddChatMessageCommand, ChatMessage>()
            .ConstructUsing((cmd, ctx) =>
            {
                return new ChatMessage(
                    id: Guid.NewGuid(),
                    sessionId: cmd.SessionId,
                    message: cmd.Content,
                    senderType: cmd.SenderType
                );
            });

        CreateMap<ChatSession, ChatSessionDto>()
            .ConstructUsing((src, ctx) => new ChatSessionDto(
                src.Id,
                src.AccountId,
                src.CreatedAt,
                src.EndTime,
                ctx.Mapper.Map<List<ChatMessageDto>>(src.Messages)
            ));

        CreateMap<ChatMessage, ChatMessageDto>()
            .ConstructUsing((src, ctx) => new ChatMessageDto(
                src.Id,
                src.SessionId,
                src.Message,
                src.SenderType,
                src.CreatedAt
            ));

        CreateMap<UpdateChatMessageCommand, ChatMessage>()
            .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.NewContent));


    }
}
