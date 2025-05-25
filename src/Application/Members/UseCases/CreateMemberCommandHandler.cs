using SharedKernel.Abstractions;
using FluentResults;
using AutoMapper;
using Domain.Members.Model;
using FluentValidation;
using Domain.Members.Repository;
using Application.Members.Commands.CreateMember;

namespace Application.Members.UseCases;

public class CreateMemberCommandHandler : ICommandHandler<CreateMemberCommand, Result<Guid>>
{
    private readonly IMemberRepository _memberRepository;

    private readonly IValidator<CreateMemberCommand> _validator;

    private readonly IMapper _mapper;
    public CreateMemberCommandHandler(
        IMemberRepository memberRepository,
        IMapper mapper,
        IValidator<CreateMemberCommand> validator
        )
    {
        _memberRepository = memberRepository;
        _validator = validator;
        _mapper = mapper;
    }
    public async Task<Result<Guid>> Handle(CreateMemberCommand command, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(command);
        if (!validationResult.IsValid)
        {
            return Result.Fail<Guid>(validationResult.Errors.Select(e => new Error(e.ErrorMessage)));
        }
        var member = _mapper.Map<Member>(command);

        await _memberRepository.Create(member, cancellationToken);
                
        return Result.Ok(member.Id);
    }
}
