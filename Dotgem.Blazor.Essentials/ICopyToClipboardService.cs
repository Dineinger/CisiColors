namespace Dotgem.Blazor.Essentials
{
    public interface ICopyToClipboardService : IAsyncDisposable
    {
        ValueTask<bool> CopyToClipboardAsync(string message);
    }
}