using SportBuddy.Core.Exceptions;

namespace SportBuddy.Core.ValueObjects;

public sealed record Location
{
    public string Value { get; }
        
    public Location(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length is > 255 or < 2)
        {
            throw new InvalidLocationException(value);
        }
            
        Value = value;
    }

    public static implicit operator Location(string value) => value is null ? null : new Location(value);

    public static implicit operator string(Location value) => value?.Value;

    public override string ToString() => Value;
}