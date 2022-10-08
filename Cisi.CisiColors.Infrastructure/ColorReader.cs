using System.Text.Json;

namespace Cisi.CisiColors.Infrastructure;

public sealed class ColorReader : ColorReaderBase, IColorReader
{
    private readonly ColorPaths _paths;

    public ColorReader(ColorPaths paths)
    {
        _paths = paths;
    }

    public override Task<ColorReaderValue> ReadCisiColorAsync()
    {
        var path = _paths.CisiPath;
        var colors = JsonSerializer.DeserializeAsync<List<ColorDefinitionJsonModel>>(File.OpenRead(path));
        return ColorReaderValue.From(colors);
    }
}
