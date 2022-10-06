using Microsoft.AspNetCore.Components;
using Dotgem.Colors;

namespace Cisi.CisiColors.BlazorServer.Shared;

public abstract class ColorTextBase : ComponentBase
{
    [Parameter, EditorRequired]
    public Color Color { get; set; }

    [Parameter]
    public bool WithDescription { get; set; } = true;

    public abstract Func<Color, string> ColorAsString { get; }
    public abstract Func<string> Description { get; }

    public string Text => WithDescription ? $"{Description()}: {ColorAsString(Color)}" : ColorAsString(Color);
}
