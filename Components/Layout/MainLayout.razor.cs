using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace StoriesOfInclusion.Components.Layout
{
    public partial class MainLayout : LayoutComponentBase, IDisposable
    {
        [Inject] private IJSRuntime JSRuntime { get; set; } = default!;

        protected override void OnInitialized()
        {
            AppState.OnChange += HandleStateChanged;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await SyncDomSettings();
            }
        }

        private async void HandleStateChanged()
        {
            await InvokeAsync(StateHasChanged);
            await SyncDomSettings();
        }

        private async Task SyncDomSettings()
        {
            try
            {
                await JSRuntime.InvokeVoidAsync("soiApp.applyTheme", AppState.Theme);
                await JSRuntime.InvokeVoidAsync("soiApp.setFontScale", AppState.FontScalePercent);
                await JSRuntime.InvokeVoidAsync("soiApp.setDyslexic", AppState.IsDyslexicFont);
                await JSRuntime.InvokeVoidAsync("soiApp.setLanguage", AppState.CurrentLanguage);
            }
            catch
            {
                // JSRuntime cannot be called before circuit completes hydration; safe to ignore
            }
        }

        public void Dispose()
        {
            AppState.OnChange -= HandleStateChanged;
        }
    }
}
