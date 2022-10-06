using Dotgem.Colors;

namespace Cisi.CisiColors;

public static class ColorFormater
{
    public const string HEX_DESCRIPTION = "Hex";

    public static string GetHex(Color color)
    {
        return $"#{color.ToArgb() - 0xff000000:X6}";
    }

    public static string GetRGB(Color color)
    {
        return $"{color.R}, {color.G}, {color.B}";
    }

    public static string GetHSL(Color color)
    {
        var hsl = color.HSL;
        return $"{hsl.H}, {hsl.S}, {hsl.L}";
    }
}
