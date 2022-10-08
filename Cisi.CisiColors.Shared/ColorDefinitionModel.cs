using System.Drawing;
using System.Text.Json.Serialization;

namespace Cisi.CisiColors;

public class ColorDefinitionJsonModel
{
    [JsonConstructor]
    public ColorDefinitionJsonModel(Color color, string name)
    {
        Name = name;
        Color = color;
    }

    public ColorDefinitionJsonModel()
    {
        Name = "[undefined]";
    }

    [JsonPropertyName("name"), JsonPropertyOrder(0)]
    public string Name { get; set; }

    [JsonPropertyName("color"), JsonPropertyOrder(1)]
    [JsonConverter(typeof(JsonColorConverter))]
    public Color Color { get; set; }
}