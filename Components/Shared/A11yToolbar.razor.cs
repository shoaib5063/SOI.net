using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace StoriesOfInclusion.Components.Shared
{
    public partial class A11yToolbar : ComponentBase, IDisposable
    {
        [Inject] private IJSRuntime JSRuntime { get; set; } = default!;

        protected override void OnInitialized()
        {
            AppState.OnChange += StateHasChanged;
        }

        private async Task HandleReadAloud()
        {
            try
            {
                await JSRuntime.InvokeVoidAsync("soiApp.toggleReadAloud", AppState.CurrentLanguage);
            }
            catch {}
        }

        public void Dispose()
        {
            AppState.OnChange -= StateHasChanged;
        }
    }
}
