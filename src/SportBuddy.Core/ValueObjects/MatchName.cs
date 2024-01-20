using SportBuddy.Core.Exceptions;

namespace SportBuddy.Core.ValueObjects;

public sealed record MatchName
{
    public string Value { get; }
        
    public MatchName(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length is > 64 or < 2)
        {
            throw new InvalidNameException(value);
        }
            
        Value = value;
    }

    public static implicit operator MatchName(string value) => value is null ? null : new MatchName(value);

    public static implicit operator string(MatchName value) => value?.Value;

    public override string ToString() => Value;
}