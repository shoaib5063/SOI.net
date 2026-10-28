using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using StoriesOfInclusion.Services;

namespace StoriesOfInclusion.Components.Layout
{
    public partial class NavMenu : ComponentBase, IDisposable
    {
        [Inject] private IJSRuntime JSRuntime { get; set; } = default!;

        private bool IsMobileMenuOpen { get; set; } = false;

        protected override void OnInitialized()
        {
            AppState.OnChange += StateHasChanged;
        }

        private void ToggleMobileMenu()
        {
            IsMobileMenuOpen = !IsMobileMenuOpen;
        }

        private void CloseMobileMenu()
        {
            IsMobileMenuOpen = false;
        }

        private async Task OpenRbacModal()
        {
            CloseMobileMenu();
            AppState.OpenRbacModal();
            try
            {
                await JSRuntime.InvokeVoidAsync("soiApp.openRbacModal");
            }
            catch {}
        }

        private string GetDashboardUrl()
        {
            return AppState.CurrentRole switch
            {
                AppRole.User => "dashboard/user",
                AppRole.Admin => "dashboard/admin",
                AppRole.SuperAdmin => "dashboard/superadmin",
                _ => ""
            };
        }

        private string GetRoleStatusClass()
        {
            return AppState.CurrentRole switch
            {
                AppRole.SuperAdmin => "superadmin",
                AppRole.Admin => "admin",
                AppRole.User => "user",
                _ => "guest"
            };
        }

        private string GetRoleIcon()
        {
            return AppState.CurrentRole switch
            {
                AppRole.SuperAdmin => "⚡",
                AppRole.Admin => "🛡️",
                AppRole.User => "👤",
                _ => "🌐"
            };
        }

        public void Dispose()
        {
            AppState.OnChange -= StateHasChanged;
        }
    }
}
