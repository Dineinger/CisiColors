using Dotgem.Colors;

namespace Cisi.CisiColors;

public sealed class ColorDefinition
{
    public ColorDefinition(string id, string name, Color color)
    {
        Id = id;
        Name = name;
        Color = color;
    }

    public string Id { get; }
    public string Name { get; }
    public Color Color { get; }
}
