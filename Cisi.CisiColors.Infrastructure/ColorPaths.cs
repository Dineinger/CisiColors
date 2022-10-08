namespace Cisi.CisiColors.Infrastructure;

public class ColorPaths
{
    private readonly string _basePath;

    public ColorPaths(string basePath)
    {
        _basePath = basePath;
    }

    public string CisiPath => Path.Combine(_basePath, "cisi", "primary.json");
}