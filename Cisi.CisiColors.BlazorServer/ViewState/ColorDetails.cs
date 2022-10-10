using System.Security.Cryptography.X509Certificates;

namespace Cisi.CisiColors.BlazorServer.ViewState;

public struct ColorDetails
{
    private const int _none = 0b_0000;
    private const int _hex = 0b_0001;
    private const int _rgb = 0b_0010;
    private const int _hsl = 0b_0100;
    private const int _cmyk = 0b_1000;

    private int _value = _none;

    public ColorDetails()
    {
    }

    public bool IsHEX => (_value & _hex) == _hex;
    public bool IsRGB => (_value & _rgb) == _rgb;
    public bool IsHSL => (_value & _hsl) == _hsl;
    public bool IsCMYK => (_value & _cmyk) == _cmyk;

    public bool Is(ColorDetail detail)
    {
        return (_value & detail.Value) == detail.Value;
    }

    public static ColorDetail HEX => ColorDetail.HEX;
    public static ColorDetail RGB => ColorDetail.RGB;
    public static ColorDetail HSL => ColorDetail.HSL;
    public static ColorDetail CMYK => ColorDetail.CMYK;

    private ColorDetails ToggleValue(ColorDetail value)
    {
        if (Is(value))
        {
            _value -= value.Value;
        }
        else
        {
            _value += value.Value;
        }
        return this;
    }

    public override bool Equals(object? obj)
    {
        return obj is ColorDetails details &&
               Equals(details);
    }

    public bool Equals(ColorDetails details)
    {
        return _value == details._value;
    }

    public override int GetHashCode()
    {
        return _value.GetHashCode();
    }

    public static bool operator ==(ColorDetails left, ColorDetails right) => left.Equals(right);
    public static bool operator !=(ColorDetails left, ColorDetails right) => !(left == right);

    public static ColorDetails operator +(ColorDetails left, ColorDetails right) => new() { _value = left._value | right._value };
    public static ColorDetails operator +(ColorDetails left, ColorDetail right) => new() { _value = left._value | right.Value };
    public static ColorDetails operator -(ColorDetails left, ColorDetail right) => new() { _value = left._value - right.Value };

    public sealed class ColorDetail
    {
        private static readonly ColorDetail _hexDetail = new(_hex);
        private static readonly ColorDetail _rgbDetail = new(_rgb);
        private static readonly ColorDetail _hslDetail = new(_hsl);
        private static readonly ColorDetail _cmykDetail = new(_cmyk);

        public readonly int Value;

        public static ColorDetail HEX => _hexDetail;
        public static ColorDetail RGB => _rgbDetail;
        public static ColorDetail HSL => _hslDetail;
        public static ColorDetail CMYK => _cmykDetail;

        private ColorDetail(int value)
        {
            Value = value;
        }
    }
}
