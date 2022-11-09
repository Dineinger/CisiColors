using Microsoft.JSInterop;

namespace Dotgem.Blazor.Essentials;

public sealed class CopyToClipboardService : IAsyncDisposable, ICopyToClipboardService
{
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

    public CopyToClipboardService(IJSRuntime jsRuntime)
    {
        _moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/Dotgem.Blazor.Essentials/copyToClipboard.js").AsTask());
    }

    public async ValueTask<bool> CopyToClipboardAsync(string message)
    {
        var module = await _moduleTask.Value;
        return await module.InvokeAsync<bool>("copyToClipboard", message);
    }

    public async ValueTask DisposeAsync()
    {
        if (_moduleTask.IsValueCreated)
        {
            var module = await _moduleTask.Value;
            await module.DisposeAsync();
        }
    }
}