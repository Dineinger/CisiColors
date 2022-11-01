namespace Cisi.CisiColors.Infrastructure;

public interface IColorReader
{
    public Task<ColorCollectionAndStatus> ReadCisiColorAsync();
}
