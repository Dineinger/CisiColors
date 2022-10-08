namespace Cisi.CisiColors.Infrastructure;

public abstract class ColorReaderBase : IColorReader
{
    public abstract Task<ColorReaderValue> ReadCisiColorAsync();
}
