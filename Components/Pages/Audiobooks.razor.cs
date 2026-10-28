using Microsoft.AspNetCore.Components;
using StoriesOfInclusion.Models;
using StoriesOfInclusion.Services;

namespace StoriesOfInclusion.Components.Pages
{
    public partial class Audiobooks : ComponentBase, IDisposable
    {
        [Inject] private ISupabaseService SupabaseService { get; set; } = default!;

        private List<Audiobook> AllAudiobooks { get; set; } = new();

        private string SelectedGrade { get; set; } = "All";
        private string SearchQuery { get; set; } = string.Empty;
        private Audiobook? ActivePlayingBook { get; set; }
        private int? ExpandedTranscriptBookId { get; set; }

        private IEnumerable<Audiobook> FilteredAudiobooks => AllAudiobooks.Where(b =>
            (SelectedGrade == "All" || b.GradeLevel == SelectedGrade) &&
            (string.IsNullOrWhiteSpace(SearchQuery) || 
             b.TitleEn.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) || 
             b.TitleBn.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)));

        protected override async Task OnInitializedAsync()
        {
            AppState.OnChange += StateHasChanged;
            AllAudiobooks = await SupabaseService.GetAudiobooksAsync();
        }

        private void FilterByClass(string grade) => SelectedGrade = grade;

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

        private string FormatDuration(int seconds) => $"{seconds / 60:00}:{seconds % 60:00}";

        public void Dispose()
        {
            AppState.OnChange -= StateHasChanged;
        }
    }
}
