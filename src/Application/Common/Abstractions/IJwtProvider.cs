using Domain.Members.Model;

namespace Application.Common.Abstractions;

public interface IJwtProvider
{
    string Generate(Member member);
}
