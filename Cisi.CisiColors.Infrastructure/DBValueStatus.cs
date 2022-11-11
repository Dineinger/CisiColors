namespace Cisi.CisiColors.Infrastructure;

public sealed record class DBValueStatus
{
    private static readonly DBValueStatus _notFount = new(false, false);
    private static readonly DBValueStatus _fountNotLoaded = new(true, false);
    private static readonly DBValueStatus _fountAndLoaded = new(true, true);

    private DBValueStatus(bool found, bool couldLoad)
    {
        Found = found;
        CouldLoad = couldLoad;
    }

    public bool Found { get; private init; }
    public bool CouldLoad { get; private init; }

    public static DBValueStatus NotFound() => _notFount;
    public static DBValueStatus FoundNotLoaded() => _fountNotLoaded;
    public static DBValueStatus FoundAndLoaded() => _fountAndLoaded;
}
