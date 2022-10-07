using System.Text.Json;

namespace Cisi.CisiColors.Infrastructure;

public class ColorReader : ColorReaderBase, IColorReader
{
    private readonly ColorPaths _paths;

    public ColorReader(ColorPaths paths)
    {
        _paths = paths;
    }

    public override Task<List<ColorDefinition>?> ReadCisiColorAsync()
    {
        var path = _paths.GetCisiPath();
        var colors = JsonSerializer.DeserializeAsync<List<ColorDefinition>>(File.OpenRead(path));
        return colors.AsTask();
    }
}
