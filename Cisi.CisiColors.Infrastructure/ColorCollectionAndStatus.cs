using System.Diagnostics.CodeAnalysis;

namespace Cisi.CisiColors.Infrastructure;

public sealed record class ColorCollectionAndStatus(DBValueStatus Status, ColorsCollection? Value)
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

    public static ColorCollectionAndStatus NotFound() => new(DBValueStatus.NotFound(), null);
    public static ColorCollectionAndStatus FoundNotLoaded() => new(DBValueStatus.FoundNotLoaded(), null);
    public static ColorCollectionAndStatus FoundAndLoaded(ColorsCollection value) => new(DBValueStatus.FoundAndLoaded(), value);
}
