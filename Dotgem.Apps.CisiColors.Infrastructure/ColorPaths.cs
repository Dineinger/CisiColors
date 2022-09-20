namespace Dotgem.Apps.CisiColors.Infrastructure;

public class ColorPaths
{
    private readonly string _basePath;

    public ColorPaths(string basePath)
    {
        _basePath = basePath;
    }

    public string GetCisiPath() => Path.Combine(_basePath, "cisi", "primary.json");
}