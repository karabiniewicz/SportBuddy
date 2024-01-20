using System.Globalization;
using SportBuddy.Core.Exceptions;

namespace SportBuddy.Core.ValueObjects;

public sealed record Charge
{
    public decimal Value { get; }

    public Charge(decimal? value)
    {
        Value = value ?? 0;
        if (Value < 0)
        {
            throw new InvalidChargeException(Value);
        }
    }

    public static implicit operator Charge(decimal? value) => new(value);

    public static implicit operator decimal(Charge value) => value.Value;

    public override string ToString() => Value.ToString(CultureInfo.InvariantCulture);
}