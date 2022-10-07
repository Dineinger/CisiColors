namespace Cisi.CisiColors.Infrastructure;

public interface IColorReader
{
    public Task<List<ColorDefinition>?> ReadCisiColorAsync();
    Task<((bool Found, bool CouldLoad), List<ColorDefinition>?)> LoadColorsAsync(Task<List<ColorDefinition>?>? getColors);
}
