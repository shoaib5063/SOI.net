using Microsoft.AspNetCore.Components;
using StoriesOfInclusion.Models;
using StoriesOfInclusion.Services;

namespace StoriesOfInclusion.Components.Pages
{
    public partial class Contact : ComponentBase, IDisposable
    {
        [Inject] private ISupabaseService SupabaseService { get; set; } = default!;

        private FormSubmission SubmissionModel { get; set; } = new();
        private bool IsSubmitted { get; set; } = false;
        private bool IsSubmitting { get; set; } = false;

        protected override void OnInitialized()
        {
            AppState.OnChange += StateHasChanged;
        }

        private async Task HandleSubmit()
        {
            IsSubmitting = true;
            await SupabaseService.SubmitFormAsync(SubmissionModel);
            IsSubmitting = false;
            IsSubmitted = true;
            SubmissionModel = new();
        }

        public void Dispose()
        {
            AppState.OnChange -= StateHasChanged;
        }
    }
}
