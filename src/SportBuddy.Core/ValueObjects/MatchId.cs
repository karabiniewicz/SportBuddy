using SportBuddy.Core.Exceptions;

namespace SportBuddy.Core.ValueObjects;

public sealed record MatchId
{
    public Guid Value { get; }

    public MatchId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidEntityIdException(value);
        }

        Value = value;
    }

    public static implicit operator Guid(MatchId date) => date.Value;
    
    public static implicit operator MatchId(Guid value) => new(value);
}