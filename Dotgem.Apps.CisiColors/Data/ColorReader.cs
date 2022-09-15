using Dotgem.Apps.CisiColors.Shared;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dotgem.Apps.CisiColors.Data;

public class ColorReader
{
    private readonly HttpClient _http;
    private readonly string _basePath;

    public ColorReader(HttpClient http, string basePath)
    {
        _http = http;
        _basePath = basePath;
    }

    public Task<List<ColorDefinition>?> ReadCisiColorAsync()
    {
        var path = GetCisiPath();
        var colors = _http.GetFromJsonAsync<List<ColorDefinition>>(path);
        return colors;
    }

    private string GetCisiPath() => Path.Combine(_basePath, "cisi", "primary.json");
}


public class ColorDefinition
{
    [JsonConstructor]
    public ColorDefinition(System.Drawing.Color color, string name)
    {
        Name = name;
        Color = color;
    }

    public ColorDefinition()
    {
        Name = "[undefined";
    }

    [JsonPropertyName("name"), JsonPropertyOrder(0)]
    public string Name { get; set; }

    [JsonPropertyName("color"), JsonPropertyOrder(1)]
    [JsonConverter(typeof(JsonColorConverter))]
    public Color Color { get; set; }

}

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
            rSpan = value.Slice(0, 2);
            gSpan = value.Slice(2, 2);
            bSpan = value.Slice(4, 2);
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
        writer.WriteStringValue(ColorTextHex.GetHex(value));
    }
}
