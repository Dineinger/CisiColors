namespace Cisi.CisiColors.Infrastructure;

public abstract class ColorReaderBase : IColorReader
{
    public abstract Task<ColorCollectionAndStatus> ReadCisiColorAsync();
}
