using System.Text.Json;

namespace Dotgem.Apps.CisiColors.Infrastructure;

public class ColorReader : IColorReader
{
    private readonly ColorPaths _paths;

    public ColorReader(ColorPaths paths)
    {
        _paths = paths;
    }

    public Task<List<ColorDefinition>?> ReadCisiColorAsync()
    {
        var path = _paths.GetCisiPath();
        var colors = JsonSerializer.DeserializeAsync<List<ColorDefinition>>(File.OpenRead(path));
        return colors.AsTask();
    }
}
