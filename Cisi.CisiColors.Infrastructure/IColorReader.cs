namespace Cisi.CisiColors.Infrastructure;

public interface IColorReader
{
    public Task<List<ColorDefinition>?> ReadCisiColorAsync();
    Task<ColorReaderValue> LoadColorsAsync(Task<List<ColorDefinition>?>? getColors);
}
