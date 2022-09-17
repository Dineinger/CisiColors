using System.Drawing;

namespace Dotgem.Apps.CisiColors;

public static class ColorFormater
{
    public static string GetHex(Color color)
    {
        return $"#{(uint)color.ToArgb() - 0xff000000:X6}";
    }
}
