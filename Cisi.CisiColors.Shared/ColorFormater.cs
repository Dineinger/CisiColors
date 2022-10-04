using System.Drawing;

namespace Cisi.CisiColors;

public static class ColorFormater
{
    public const string HEX_DESCRIPTION = "Hex";

    public static string GetHex(Color color)
    {
        return $"#{(uint)color.ToArgb() - 0xff000000:X6}";
    }

    public static string GetRGB(Color color)
    {
        return $"{color.R}, {color.G}, {color.B}";
    }

    public static string GetHSL(Color color)
    {
        var (R, G, B) = (color.R, color.G, color.B);
        
        float _R = R / 255f;
        float _G = G / 255f;
        float _B = B / 255f;

        float _Min = Math.Min(Math.Min(_R, _G), _B);
        float _Max = Math.Max(Math.Max(_R, _G), _B);
        float _Delta = _Max - _Min;

        float H = 0;
        float S = 0;
        float L = (float)((_Max + _Min) / 2.0f);

        if (_Delta != 0)
        {
            if (L < 0.5f)
            {
                S = (float)(_Delta / (_Max + _Min));
            }
            else
            {
                S = (float)(_Delta / (2.0f - _Max - _Min));
            }

            if (_R == _Max)
            {
                H = (_G - _B) / _Delta;
            }
            else if (_G == _Max)
            {
                H = 2f + (_B - _R) / _Delta;
            }
            else if (_B == _Max)
            {
                H = 4f + (_R - _G) / _Delta;
            }
            H *= 60f;
            if (H < 0) H += 360;
        }

        return $"{H}, {S}, {L} ";
    }
}
