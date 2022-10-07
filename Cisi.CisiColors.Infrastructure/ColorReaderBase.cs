namespace Cisi.CisiColors.Infrastructure;

public abstract class ColorReaderBase : IColorReader
{
    public async Task<((bool Found, bool CouldLoad), List<ColorDefinition>?)> LoadColorsAsync(Task<List<ColorDefinition>?>? getColors)
    {
        if (getColors is null) return ((false, false), null);

        var value = await getColors;

        return value is null
            ? ((true, false), null)
            : ((true, true), value);
    }

    public abstract Task<List<ColorDefinition>?> ReadCisiColorAsync();
}
