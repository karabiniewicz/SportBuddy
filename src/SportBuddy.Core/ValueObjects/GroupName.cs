using SportBuddy.Core.Exceptions;

namespace SportBuddy.Core.ValueObjects;

public sealed record GroupName
{
    public string Value { get; }
        
    public GroupName(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length is > 64 or < 2)
        {
            throw new InvalidNameException(value);
        }
            
        Value = value;
    }

    public static implicit operator GroupName(string value) => value is null ? null : new GroupName(value);

    public static implicit operator string(GroupName value) => value?.Value;

    public override string ToString() => Value;
}