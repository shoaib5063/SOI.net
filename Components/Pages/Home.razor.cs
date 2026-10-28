using Microsoft.AspNetCore.Components;
using StoriesOfInclusion.Models;
using StoriesOfInclusion.Services;

namespace StoriesOfInclusion.Components.Pages
{
    public partial class Home : ComponentBase, IDisposable
    {
        [Inject] private ISupabaseService SupabaseService { get; set; } = default!;

        private List<Audiobook> FeaturedAudiobooks { get; set; } = new();
        private Audiobook? ActivePlayingBook { get; set; }
        private int? ExpandedTranscriptBookId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            AppState.OnChange += StateHasChanged;
            FeaturedAudiobooks = await SupabaseService.GetAudiobooksAsync();
        }

        private void PlayAudiobook(Audiobook book)
        {
            ActivePlayingBook = book;
        }

        private void ClosePlayer()
        {
            ActivePlayingBook = null;
        }

        private void ToggleTranscript(int id)
        {
            ExpandedTranscriptBookId = ExpandedTranscriptBookId == id ? null : id;
        }

        private string FormatDuration(int seconds)
        {
            var m = seconds / 60;
            var s = seconds % 60;
            return $"{m:00}:{s:00}";
        }

        public void Dispose()
        {
            AppState.OnChange -= StateHasChanged;
        }
    }
}
