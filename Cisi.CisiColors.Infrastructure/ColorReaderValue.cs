using System.Diagnostics.CodeAnalysis;

namespace Cisi.CisiColors.Infrastructure;

public readonly record struct ColorCollectionAndStatus(ColorReaderStatus Status, ColorsCollection? Value)
{
    public bool TryCouldLoad([NotNullWhen(true)] out ColorsCollection? value)
    {
        if (Status.CouldLoad && Value is not null)
        {
            value = Value;
            return true;
        }
        value = null;
        return false;
    }

    public static ColorCollectionAndStatus NotFound() => new(ColorReaderStatus.NotFound(), null);
    public static ColorCollectionAndStatus FoundNotLoaded() => new(ColorReaderStatus.FoundNotLoaded(), null);
    public static ColorCollectionAndStatus FoundAndLoaded(ColorsCollection value) => new(ColorReaderStatus.FoundAndLoaded(), value);
}
