using Microsoft.AspNetCore.Components;
using StoriesOfInclusion.Models;
using StoriesOfInclusion.Services;

namespace StoriesOfInclusion.Components.Pages
{
    public partial class Team : ComponentBase, IDisposable
    {
        [Inject] private MemberPortfolioService PortfolioService { get; set; } = default!;

        private List<MemberPortfolio> Members => PortfolioService.GetAllMembers();

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