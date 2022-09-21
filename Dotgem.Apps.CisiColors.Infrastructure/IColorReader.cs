namespace Cisi.CisiColors.Infrastructure;

public interface IColorReader
{
    public Task<List<ColorDefinition>?> ReadCisiColorAsync();
}
