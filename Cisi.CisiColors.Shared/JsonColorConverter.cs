using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cisi.CisiColors;

internal class JsonColorConverter : JsonConverter<Color>
{
    public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        ReadOnlySpan<char> value = reader.GetString();
        if (value.Length == 0)
        {
            return Color.FromArgb(0, 0, 0);
        }

        ReadOnlySpan<char> rSpan = new();
        ReadOnlySpan<char> gSpan = new();
        ReadOnlySpan<char> bSpan = new();

        if (value.Length == 6)
        {
            rSpan = value[..2];
            gSpan = value[2..4];
            bSpan = value[4..6];
        }

        try
        {
            var r = int.Parse(rSpan, System.Globalization.NumberStyles.HexNumber);
            var g = int.Parse(gSpan, System.Globalization.NumberStyles.HexNumber);
            var b = int.Parse(bSpan, System.Globalization.NumberStyles.HexNumber);

            return Color.FromArgb(r, g, b);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(ColorFormater.GetHex(value));
    }
}
