using System.Net.Http.Json;

namespace Dotgem.Apps.CisiColors.Infrastructure;

public class ColorReaderHttp
{
    private readonly HttpClient _http;
    private readonly string _basePath;

    public ColorReaderHttp(HttpClient http, string basePath)
    {
        _http = http;
        _basePath = basePath;
    }

    public Task<List<ColorDefinition>?> ReadCisiColorAsync()
    {
        var path = GetCisiPath();
        var colors = _http.GetFromJsonAsync<List<ColorDefinition>>(path);
        return colors;
    }

    private string GetCisiPath() => Path.Combine(_basePath, "cisi", "primary.json");
}
