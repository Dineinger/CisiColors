namespace Cisi.CisiColors.BlazorServer.ViewState;

public sealed class ColorDetail
{
    private const int _default = 0b_0000;
    private const int _hex = 0b_0001;
    private const int _rgb = 0b_0010;
    private const int _hsl = 0b_0100;
    private const int _cmyk = 0b_1000;
    private static readonly ColorDetail _defaultDetail = new(_default);
    private static readonly ColorDetail _hexDetail = new(_hex);
    private static readonly ColorDetail _rgbDetail = new(_rgb);
    private static readonly ColorDetail _hslDetail = new(_hsl);
    private static readonly ColorDetail _cmykDetail = new(_cmyk);

    public static ColorDetail Default => _defaultDetail;
    public static ColorDetail HEX => _hexDetail;
    public static ColorDetail RGB => _rgbDetail;
    public static ColorDetail HSL => _hslDetail;
    public static ColorDetail CMYK => _cmykDetail;

    public readonly int Value;

    private ColorDetail(int value)
    {
        Value = value;
    }
}
