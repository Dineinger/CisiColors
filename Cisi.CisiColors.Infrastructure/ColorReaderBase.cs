namespace Cisi.CisiColors.Infrastructure;

public abstract class ColorReaderBase : IColorReader
{
    public async Task<ColorReaderValue> LoadColorsAsync(Task<List<ColorDefinition>?>? getColors)
    {
        if (getColors is null) return ColorReaderValue.NotFound();

        var value = await getColors;

        return value is null
            ? ColorReaderValue.FoundNotLoaded()
            : ColorReaderValue.FoundAndLoaded(value);
    }

    public abstract Task<List<ColorDefinition>?> ReadCisiColorAsync();
}
