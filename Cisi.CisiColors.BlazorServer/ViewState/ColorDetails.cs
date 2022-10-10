namespace Cisi.CisiColors.BlazorServer.ViewState;

public struct ColorDetails
{
    private int _value = ColorDetail.Default.Value;

    public ColorDetails()
    {
    }

    private ColorDetails(ColorDetail detail)
    {
        _value = detail.Value;
    }

    public bool IsHexOn => IsOn(ColorDetail.HEX);
    public bool IsRgbOn => IsOn(ColorDetail.RGB);
    public bool IsHslOn => IsOn(ColorDetail.HSL);
    public bool IsCmykOn => IsOn(ColorDetail.CMYK);

    public bool IsOn(ColorDetail detail)
    {
        return (_value & detail.Value) == detail.Value;
    }

    public bool IsAnyOn()
    {
        return _value != ColorDetail.Default.Value;
    }

    public ColorDetails ToggleValue(ColorDetail value)
    {
        if (IsOn(value))
        {
            _value -= value.Value;
        }
        else
        {
            _value += value.Value;
        }
        return this;
    }

    public static ColorDetails HEX => new(ColorDetail.HEX);
    public static ColorDetails RGB => new(ColorDetail.RGB);
    public static ColorDetails HSL => new(ColorDetail.HSL);
    public static ColorDetails CMYK => new(ColorDetail.CMYK);

    public override bool Equals(object? obj) => obj is ColorDetails details && Equals(details);
    public bool Equals(ColorDetails details) => _value == details._value;
    public override int GetHashCode() => _value.GetHashCode();

    public static bool operator ==(ColorDetails left, ColorDetails right) => left.Equals(right);
    public static bool operator !=(ColorDetails left, ColorDetails right) => !(left == right);

    public static ColorDetails operator +(ColorDetails left, ColorDetails right) => new() { _value = left._value | right._value };
    public static ColorDetails operator +(ColorDetails left, ColorDetail right) => new() { _value = left._value | right.Value };
    public static ColorDetails operator -(ColorDetails left, ColorDetail right) => new() { _value = left._value - right.Value };
}
