using System.Net.Http.Json;

namespace Cisi.CisiColors.Infrastructure;

public class ColorReaderHttp : IColorReader
{
    private readonly HttpClient _http;
    private readonly ColorPaths _paths;

    public ColorReaderHttp(HttpClient http, ColorPaths paths)
    {
        _http = http;
        _paths = paths;
    }

    public Task<List<ColorDefinition>?> ReadCisiColorAsync()
    {
        var path = _paths.GetCisiPath();
        var basePath = _http.BaseAddress;
        var colors = _http.GetFromJsonAsync<List<ColorDefinition>>(path);
        return colors;
    }
}
