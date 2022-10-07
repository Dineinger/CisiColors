using System.Diagnostics.CodeAnalysis;

namespace Cisi.CisiColors.Infrastructure;

public readonly record struct ColorReaderValue(ColorReaderStatus Status, List<ColorDefinition>? Value)
{
    public bool TryCouldLoad([NotNullWhen(true)] out List<ColorDefinition>? value)
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
    public static ColorReaderValue FoundAndLoaded(List<ColorDefinition> value) => new(ColorReaderStatus.FoundAndLoaded(), value);
    
    public static async Task<ColorReaderValue> From(ValueTask<List<ColorDefinition>?> task)
    {
        var value = await task;

        return value is null
            ? FoundNotLoaded()
            : FoundAndLoaded(value);
    }
    
    public static async Task<ColorReaderValue> From(Task<List<ColorDefinition>?>? task)
    {
        if (task is null) return NotFound();

        var value = await task;

        return value is null
            ? FoundNotLoaded()
            : FoundAndLoaded(value);
    }
}
