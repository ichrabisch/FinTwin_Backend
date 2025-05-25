namespace SharedKernel.Abstractions;

public interface ICommandPublisher
{
    public Task<TResponse> Publish<TResponse, TCommand>(TCommand command, CancellationToken cancellationToken = default)
        where TCommand : class, ICommand<TResponse>;
}