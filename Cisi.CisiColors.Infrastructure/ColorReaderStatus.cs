namespace Cisi.CisiColors.Infrastructure;

public readonly record struct ColorReaderStatus(bool Found, bool CouldLoad)
{
    public static ColorReaderStatus NotFound() => new(false, false);
    public static ColorReaderStatus FoundNotLoaded() => new(true, false);
    public static ColorReaderStatus FoundAndLoaded() => new(true, true);
}
