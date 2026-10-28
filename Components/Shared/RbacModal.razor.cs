using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using StoriesOfInclusion.Services;

namespace StoriesOfInclusion.Components.Shared
{
    public partial class RbacModal : ComponentBase, IDisposable
    {
        [Inject] private IJSRuntime JSRuntime { get; set; } = default!;

        protected override void OnInitialized()
        {
            AppState.OnChange += StateHasChanged;
        }

        private void SelectRole(AppRole role)
        {
            AppState.SetRole(role);
        }

        private async Task CloseModal()
        {
            AppState.CloseRbacModal();
            try
            {
                await JSRuntime.InvokeVoidAsync("soiApp.closeRbacModal");
            }
            catch {}
        }

        private async Task NavigateToDashboard()
        {
            await CloseModal();
            var target = AppState.CurrentRole switch
            {
                AppRole.User => "/dashboard/user",
                AppRole.Admin => "/dashboard/admin",
                AppRole.SuperAdmin => "/dashboard/superadmin",
                _ => "/"
            };
            Navigation.NavigateTo(target);
        }

        public void Dispose()
        {
            AppState.OnChange -= StateHasChanged;
        }
    }
}
