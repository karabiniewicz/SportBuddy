using SportBuddy.Core.Exceptions;

namespace SportBuddy.Core.ValueObjects;

public sealed record GroupDescription
{
    public string Value { get; }

    public GroupDescription(string value)
    {
        if (value?.Length > 255)
        {
            throw new InvalidDescriptionException(value);
        }

        Value = value;
    }

    public static implicit operator GroupDescription(string value) => value is null ? null : new GroupDescription(value);

    public static implicit operator string(GroupDescription value) => value?.Value;

    public override string ToString() => Value;
}