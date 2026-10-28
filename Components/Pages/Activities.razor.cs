using Microsoft.AspNetCore.Components;

namespace StoriesOfInclusion.Components.Pages
{
    public partial class Activities : ComponentBase, IDisposable
    {
        protected override void OnInitialized()
        {
            AppState.OnChange += StateHasChanged;
        }

        public void Dispose()
        {
            AppState.OnChange -= StateHasChanged;
        }
    }
}
