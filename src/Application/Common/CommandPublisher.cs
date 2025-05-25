using SharedKernel.Abstractions;

namespace Application.Common;

public sealed class CommandPublisher(IServiceProvider serviceProvider) : ICommandPublisher
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task<TResponse> Publish<TResponse, TCommand>(TCommand command, CancellationToken cancellationToken)
         where TCommand : class, ICommand<TResponse>
    {
        var useCaseHandler =
           (ICommandHandler<TCommand, TResponse>?)
           _serviceProvider.GetService(typeof(ICommandHandler<TCommand, TResponse>));


        return useCaseHandler == null
            ? throw new InvalidOperationException($"No handler registered for use case of type {typeof(TCommand)}")
            : await useCaseHandler.Handle(command, cancellationToken);
    }
}
