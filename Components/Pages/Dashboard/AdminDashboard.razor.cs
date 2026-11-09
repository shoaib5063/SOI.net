using Microsoft.AspNetCore.Components;
using StoriesOfInclusion.Models;
using StoriesOfInclusion.Services;

namespace StoriesOfInclusion.Components.Pages.Dashboard
{
    public partial class AdminDashboard : ComponentBase, IDisposable
    {
        [Inject] private ISupabaseService SupabaseService { get; set; } = default!;

        public class MemberPortfolioItem
        {
            public string Name { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Title { get; set; } = string.Empty;
            public string Status { get; set; } = "Pending";
        }

        public class AudiobookItem
        {
            public string Title { get; set; } = string.Empty;
            public string Grade { get; set; } = "Primary Class 1";
            public string Language { get; set; } = "Bangla";
            public string Duration { get; set; } = "15 mins";
            public string YouTubeUrl { get; set; } = "https://www.youtube.com/watch?v=0bT-30FkF6E";
        }

        private string ActiveTab { get; set; } = "inquiries"; // "inquiries", "portfolios", "audiobooks", "admin-guide"
        private string SubmissionFilter { get; set; } = "all";

        private bool IsAddMemberOpen { get; set; } = false;
        private string NewMemberName { get; set; } = "";
        private string NewMemberEmail { get; set; } = "";
        private string NewMemberTitle { get; set; } = "";

        private bool IsAddAudiobookOpen { get; set; } = false;
        private string NewAudioTitle { get; set; } = "";
        private string NewAudioYouTubeUrl { get; set; } = "https://www.youtube.com/watch?v=0bT-30FkF6E";
        private string NewAudioGrade { get; set; } = "Primary Class 1";
        private string NewAudioLanguage { get; set; } = "Bangla";
        private string NewAudioDuration { get; set; } = "15 mins";

        private AudiobookItem? ActivePlayingAudiobook { get; set; }

        private bool ShowAlert { get; set; } = false;
        private string AlertMessage { get; set; } = "";

        private List<FormSubmission> Submissions { get; set; } = new();

        private List<MemberPortfolioItem> Portfolios { get; set; } = new()
        {
            new() { Name = "Tanvir Hasan", Email = "tanvir.hasan@member.org", Title = "Audiobook Voice Artist", Status = "Pending" },
            new() { Name = "Salma Begum", Email = "salma.begum@teacher.org", Title = "Special Needs Educator", Status = "Pending" },
            new() { Name = "Iffat Nusaiba", Email = "nusaibaiffaat3@gmail.com", Title = "Co-Founder & Director", Status = "Approved" },
            new() { Name = "Shoaib Ahmed", Email = "shoaibahmed.5063@gmail.com", Title = "Head of Assistive Tech", Status = "Approved" }
        };

        private List<AudiobookItem> Audiobooks { get; set; } = new();

        private List<FormSubmission> FilteredSubmissions
        {
            get
            {
                if (SubmissionFilter == "all") return Submissions;
                if (SubmissionFilter == "Contacted" || SubmissionFilter == "New" || SubmissionFilter == "Under Review")
                    return Submissions.Where(s => s.Status == SubmissionFilter).ToList();
                return Submissions.Where(s => s.SubmissionType.Contains(SubmissionFilter, StringComparison.OrdinalIgnoreCase)).ToList();
            }
        }

        private int PendingPortfoliosCount => Portfolios.Count(p => p.Status == "Pending");

        protected override async Task OnInitializedAsync()
        {
            AppState.OnChange += StateHasChanged;
            await LoadSubmissionsAsync();
        }

        private async Task LoadSubmissionsAsync()
        {
            Submissions = await SupabaseService.GetSubmissionsAsync();
        }

        private async void TriggerAlert(string msg)
        {
            AlertMessage = msg;
            ShowAlert = true;
            StateHasChanged();
            await Task.Delay(3500);
            ShowAlert = false;
            StateHasChanged();
        }

        private void ToggleAddMemberModal() => IsAddMemberOpen = !IsAddMemberOpen;
        private void ToggleAddAudiobookModal() => IsAddAudiobookOpen = !IsAddAudiobookOpen;

        private void PlayAudiobook(AudiobookItem item)
        {
            ActivePlayingAudiobook = item;
        }

        private void ClosePlayer()
        {
            ActivePlayingAudiobook = null;
        }

        private async Task MarkContacted(FormSubmission s)
        {
            await SupabaseService.UpdateSubmissionStatusAsync(s.Id, "Contacted");
            await LoadSubmissionsAsync();
            TriggerAlert($"✓ Marked inquiry from {s.FullName} as Contacted.");
        }

        private async Task MarkUnderReview(FormSubmission s)
        {
            await SupabaseService.UpdateSubmissionStatusAsync(s.Id, "Under Review");
            await LoadSubmissionsAsync();
            TriggerAlert($"✓ Marked inquiry from {s.FullName} as Under Review.");
        }

        private async Task DeleteSubmission(int id)
        {
            var item = Submissions.FirstOrDefault(s => s.Id == id);
            await SupabaseService.DeleteSubmissionAsync(id);
            await LoadSubmissionsAsync();
            if (item != null)
            {
                TriggerAlert($"✓ Removed submission from {item.FullName}.");
            }
        }

        private async Task ClearContactedSubmissions()
        {
            var contacted = Submissions.Where(s => s.Status == "Contacted").ToList();
            foreach (var c in contacted)
            {
                await SupabaseService.DeleteSubmissionAsync(c.Id);
            }
            await LoadSubmissionsAsync();
            TriggerAlert($"✓ Cleared {contacted.Count} contacted inquiries from inbox.");
        }

        private void ApprovePortfolio(MemberPortfolioItem item)
        {
            item.Status = "Approved";
            TriggerAlert($"✓ Approved member portfolio for {item.Name}! It is now visible publicly.");
        }

        private void RejectPortfolio(MemberPortfolioItem item)
        {
            item.Status = "Rejected";
            TriggerAlert($"✓ Rejected portfolio for {item.Name}.");
        }

        private void DeletePortfolio(MemberPortfolioItem item)
        {
            Portfolios.Remove(item);
            TriggerAlert($"✓ Removed portfolio record for {item.Name}.");
        }

        private void CreateMemberPortfolio()
        {
            if (string.IsNullOrWhiteSpace(NewMemberName) || string.IsNullOrWhiteSpace(NewMemberEmail))
            {
                TriggerAlert("⚠️ Please enter a member name and email.");
                return;
            }

            Portfolios.Insert(0, new MemberPortfolioItem
            {
                Name = NewMemberName.Trim(),
                Email = NewMemberEmail.Trim(),
                Title = string.IsNullOrWhiteSpace(NewMemberTitle) ? "Educator & Contributor" : NewMemberTitle.Trim(),
                Status = "Approved"
            });

            TriggerAlert($"✓ Added & published member portfolio for {NewMemberName}!");
            NewMemberName = "";
            NewMemberEmail = "";
            NewMemberTitle = "";
            IsAddMemberOpen = false;
        }

        private void CreateAudiobook()
        {
            if (string.IsNullOrWhiteSpace(NewAudioTitle))
            {
                TriggerAlert("⚠️ Please enter an audiobook title.");
                return;
            }

            if (string.IsNullOrWhiteSpace(NewAudioYouTubeUrl))
            {
                TriggerAlert("⚠️ Please provide a YouTube video link.");
                return;
            }

            Audiobooks.Insert(0, new AudiobookItem
            {
                Title = NewAudioTitle.Trim(),
                Grade = NewAudioGrade,
                Language = NewAudioLanguage,
                Duration = string.IsNullOrWhiteSpace(NewAudioDuration) ? "15 mins" : NewAudioDuration.Trim(),
                YouTubeUrl = NewAudioYouTubeUrl.Trim()
            });

            TriggerAlert($"✓ Published YouTube audiobook: '{NewAudioTitle}'!");
            NewAudioTitle = "";
            NewAudioYouTubeUrl = "https://www.youtube.com/watch?v=0bT-30FkF6E";
            IsAddAudiobookOpen = false;
        }

        private void DeleteAudiobook(AudiobookItem item)
        {
            Audiobooks.Remove(item);
            TriggerAlert($"✓ Deleted audiobook '{item.Title}'.");
        }

        public void Dispose()
        {
            AppState.OnChange -= StateHasChanged;
        }
    }
}
