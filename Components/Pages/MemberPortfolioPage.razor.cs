using Microsoft.AspNetCore.Components;
using StoriesOfInclusion.Models;
using StoriesOfInclusion.Services;

namespace StoriesOfInclusion.Components.Pages
{
    public partial class MemberPortfolioPage : ComponentBase, IDisposable
    {
        [Parameter] public string Slug { get; set; } = "";

        private MemberPortfolio? Member { get; set; }
        private string SelectedBioLang { get; set; } = "en";

        private bool CanEditPortfolio
        {
            get
            {
                if (AppState.CurrentRole == AppRole.SuperAdmin || AppState.CurrentRole == AppRole.Admin)
                    return true;

                if (AppState.CurrentUser != null && Member != null)
                {
                    return string.Equals(AppState.CurrentUser.Email, Member.Email, StringComparison.OrdinalIgnoreCase);
                }

                return false;
            }
        }

        protected override void OnInitialized()
        {
            AppState.OnChange += StateHasChanged;
        }

        protected override void OnParametersSet()
        {
            Member = PortfolioService.GetBySlug(Slug);
            SelectedBioLang = AppState.CurrentLanguage == "bn" ? "bn" : "en";
        }

        private string GetRoleCategoryDisplay(string role)
        {
            return role.ToLowerInvariant() switch
            {
                "superadmin" => "SuperAdmin Leadership",
                "admin" => "Staff Administrator",
                _ => "Verified Member Contributor"
            };
        }

        public void Dispose()
        {
            AppState.OnChange -= StateHasChanged;
        }
    }
}
