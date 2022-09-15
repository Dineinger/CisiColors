namespace Dotgem.Apps.CisiColors.Infrastructure;

public class ColorReader
{
    private readonly string _basePath;

    public ColorReader(string basePath)
    {
        _basePath = basePath;
    }

    public List<ColorDefinition> ReadCisiColors()
    {
        return new List<ColorDefinition>();
    }
}

public class ColorDefinition
{

}