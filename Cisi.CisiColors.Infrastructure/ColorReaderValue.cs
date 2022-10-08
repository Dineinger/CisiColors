using System.Diagnostics.CodeAnalysis;

namespace Cisi.CisiColors.Infrastructure;

public readonly record struct ColorReaderValue(ColorReaderStatus Status, ColorsCollection? Value)
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

    public static ColorReaderValue NotFound() => new(ColorReaderStatus.NotFound(), null);
    public static ColorReaderValue FoundNotLoaded() => new(ColorReaderStatus.FoundNotLoaded(), null);
    public static ColorReaderValue FoundAndLoaded(ColorsCollection value) => new(ColorReaderStatus.FoundAndLoaded(), value);

    public static async Task<ColorReaderValue> From(ValueTask<List<ColorDefinitionJsonModel>?> task)
    {
        var value = await task;

        return From(value);
    }
    
    public static async Task<ColorReaderValue> From(Task<List<ColorDefinitionJsonModel>?>? task)
    {
        if (task is null) return NotFound();

        var value = await task;

        return From(value);
    }

    public static ColorReaderValue From(List<ColorDefinitionJsonModel>? value) =>
        value is null
            ? FoundNotLoaded()
            : FoundAndLoaded(
                    ColorsCollection.From(value)
                );
}
