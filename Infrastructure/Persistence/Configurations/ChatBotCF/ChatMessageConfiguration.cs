using Domain.ChatBot.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.ChatBot.ValueObjects;

namespace Infrastructure.Persistence.Configurations.ChatBotCF;

public class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
{
    public void Configure(EntityTypeBuilder<ChatMessage> builder)
    {
        builder.ToTable("ChatMessages");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.SessionId)
            .IsRequired();

        builder.Property(x => x.SenderType)
            .IsRequired()
            .HasConversion(
                v => v.ToString(),
                v => (SenderType)Enum.Parse(typeof(SenderType), v)
            );

        builder.Property(x => x.Message)
            .IsRequired()
            .HasMaxLength(4096); 

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.HasOne(x => x.ChatSession)
            .WithMany(s => s.Messages)
            .HasForeignKey(x => x.SessionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.SessionId);

        builder.HasIndex(x => new { x.SessionId, x.SenderType });
    }
}