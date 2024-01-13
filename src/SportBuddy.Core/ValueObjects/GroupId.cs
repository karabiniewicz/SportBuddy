using SportBuddy.Core.Exceptions;

namespace SportBuddy.Core.ValueObjects;

public sealed record GroupId
{
    public Guid Value { get; }

    public GroupId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidEntityIdException(value);
        }

        Value = value;
    }

    public static implicit operator Guid(GroupId date) => date.Value;
    
    public static implicit operator GroupId(Guid value) => new(value);
}