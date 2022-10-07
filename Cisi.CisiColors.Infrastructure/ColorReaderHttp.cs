using System.Net.Http.Json;

namespace Cisi.CisiColors.Infrastructure;

[Obsolete("This class must be tested before being used.")]
public class ColorReaderHttp : ColorReaderBase, IColorReader
{
    private readonly HttpClient _http;
    private readonly ColorPaths _paths;

    public ColorReaderHttp(HttpClient http, ColorPaths paths)
    {
        _http = http;
        _paths = paths;
    }

    public override Task<ColorReaderValue> ReadCisiColorAsync()
    {
        var path = _paths.CisiPath;
        var colors = _http.GetFromJsonAsync<List<ColorDefinition>>(path);
        return ColorReaderValue.From(colors);
    }
}
