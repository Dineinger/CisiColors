using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Cisi.CisiColors;

public sealed class ColorsCollection : ICollection<ColorDefinition>
{
    private readonly List<ColorDefinition> _values;

    private ColorsCollection(string title, List<ColorDefinition> values)
    {
        _values = values;
        Title = title;
    }

    public string Title { get; }

    public int Count => _values.Count;

    public bool IsReadOnly => ((ICollection<ColorDefinition>)_values).IsReadOnly;

    public static ColorsCollection From(string title, List<ColorDefinition> list)
    {
        return new ColorsCollection(title, list);
    }

    public static ColorsCollection Empty()
    {
        return new("unknown", new List<ColorDefinition>());
    }

    public void Add(ColorDefinition item)
    {
        _values.Add(item);
    }

    public bool Remove(ColorDefinition item)
    {
        return _values.Remove(item);
    }

    public void Clear()
    {
        _values.Clear();
    }

    public bool Contains(ColorDefinition item)
    {
        return _values.Contains(item);
    }

    public void CopyTo(ColorDefinition[] array, int arrayIndex)
    {
        _values.CopyTo(array, arrayIndex);
    }

    public IEnumerator<ColorDefinition> GetEnumerator()
    {
        return _values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _values.GetEnumerator();
    }
}
