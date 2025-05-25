using SharedKernel.Abstractions;
using System.Linq.Expressions;

namespace SharedKernel.Common;
public class CommandBuilder<TCommand> where TCommand : class, new()
{
    private readonly Dictionary<string, object> _propertyValues = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

    public CommandBuilder<TCommand> With<TProperty>(Expression<Func<TCommand, TProperty>> property, TProperty value)
    {
        if (!(property.Body is MemberExpression memberExpression))
        {
            throw new ArgumentException("Expression must be a member access", nameof(property));
        }
        string propertyName = memberExpression.Member.Name;
        _propertyValues[propertyName] = value;
        return this;
    }

    public TCommand Build()
    {
        var command = new TCommand();
        var properties = typeof(TCommand).GetProperties();

        foreach (var prop in properties)
        {
            if (_propertyValues.TryGetValue(prop.Name, out var value))
            {
                prop.SetValue(command, value);
            }
        }

        return command;
    }
}