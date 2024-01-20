using SportBuddy.Core.Exceptions;

namespace SportBuddy.Core.ValueObjects;

public sealed record MatchLimit
{
    public int Value { get; }
        
    public MatchLimit(int? value)
    {
        Value = value ?? 0;
        if (Value < 0)
        {
            throw new InvalidLimitException(Value);
        }
    }

    public static implicit operator MatchLimit(int? value) => new(value);

    public static implicit operator int(MatchLimit value) => value.Value;

    public override string ToString() => Value.ToString();
}