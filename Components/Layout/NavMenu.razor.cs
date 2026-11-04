using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using StoriesOfInclusion.Services;

namespace StoriesOfInclusion.Components.Layout
{
    public partial class NavMenu : ComponentBase, IDisposable
    {
        [Inject] private IJSRuntime JSRuntime { get; set; } = default!;
        [Inject] private NavigationManager Navigation { get; set; } = default!;

        private bool IsMobileMenuOpen { get; set; } = false;
        private bool IsActivitiesDropdownOpen { get; set; } = false;
        private bool _isLockedByClick = false;
        private System.Threading.Timer? _closeTimer;

        private bool IsActivitiesActive
        {
            get
            {
                try
                {
                    var uri = Navigation.Uri.ToLowerInvariant();
                    return uri.Contains("/activities");
                }
                catch
                {
                    return false;
                }
            }
        }

        protected override void OnInitialized()
        {
            AppState.OnChange += StateHasChanged;
        }

        private void ToggleActivitiesDropdown()
        {
            if (IsActivitiesDropdownOpen)
            {
                IsActivitiesDropdownOpen = false;
                _isLockedByClick = false;
            }
            else
            {
                IsActivitiesDropdownOpen = true;
                _isLockedByClick = true;
            }
        }

        private void OnActivitiesMouseEnter()
        {
            _closeTimer?.Dispose();
            _closeTimer = null;
            IsActivitiesDropdownOpen = true;
        }

        private void OnActivitiesMouseLeave()
        {
            if (_isLockedByClick) return; // Keep open if user clicked it open

            _closeTimer?.Dispose();
            _closeTimer = new System.Threading.Timer(_ =>
            {
                InvokeAsync(() =>
                {
                    if (!_isLockedByClick)
                    {
                        IsActivitiesDropdownOpen = false;
                        StateHasChanged();
                    }
                });
            }, null, 650, System.Threading.Timeout.Infinite);
        }

        private void CloseActivitiesDropdown()
        {
            _closeTimer?.Dispose();
            _closeTimer = null;
            _isLockedByClick = false;
            IsActivitiesDropdownOpen = false;
        }

        private void CloseAllMenus()
        {
            _closeTimer?.Dispose();
            _closeTimer = null;
            _isLockedByClick = false;
            IsActivitiesDropdownOpen = false;
            IsMobileMenuOpen = false;
        }

        private void ToggleMobileMenu()
        {
            IsMobileMenuOpen = !IsMobileMenuOpen;
        }

        private void CloseMobileMenu()
        {
            _closeTimer?.Dispose();
            _closeTimer = null;
            _isLockedByClick = false;
            IsMobileMenuOpen = false;
            IsActivitiesDropdownOpen = false;
        }

        private void HandleLogout()
        {
            CloseAllMenus();
            AppState.Logout();
            Navigation.NavigateTo("");
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

        public void Dispose()
        {
            AppState.OnChange -= StateHasChanged;
            _closeTimer?.Dispose();
        }
    }
}
