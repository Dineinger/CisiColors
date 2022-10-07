using Dotgem.Colors;

namespace Cisi.CisiColors;

public sealed class ColorDefinition
{
    private ColorDefinition(string name, Color color)
    {
        Name = name;
        Color = color;
    }

    public string Name { get; }
    public Color Color { get; }

    public static ColorDefinition From(ColorDefinitionJsonModel unverified)
    {
        // verification

        return new ColorDefinition(unverified.Name, unverified.Color);
    }
}
