using Microsoft.AspNetCore.Components;
using StoriesOfInclusion.Models;
using StoriesOfInclusion.Services;

namespace StoriesOfInclusion.Components.Pages.Dashboard
{
    public partial class UserDashboard : ComponentBase, IDisposable
    {
        [Inject] private ISupabaseService SupabaseService { get; set; } = default!;

        public class ContributedWorkItem
        {
            public string Title { get; set; } = string.Empty;
            public string Category { get; set; } = "Audiobook Production";
            public string Date { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
            public string Status { get; set; } = "Published";
        }

        private string ActiveTab { get; set; } = "profile"; // "profile", "showcase", "submit"

        private Profile ProfileModel { get; set; } = new()
        {
            Id = Guid.NewGuid(),
            FullName = "Iffat Nusaiba",
            Email = "nusaibaiffaat3@gmail.com",
            Role = "user",
            BioEn = "Co-Founder passionate about accessible education and assistive technology for primary school students in Bangladesh.",
            BioBn = "সহ-প্রতিষ্ঠাতা, যিনি বাংলাদেশে বিশেষ চাহিদাসম্পন্ন শিশুদের প্রাথমিক স্তরের অন্তর্ভুক্তিমূলক শিক্ষা নিয়ে কাজ করছেন।",
            IsPortfolioApproved = true
        };

        private List<ContributedWorkItem> ContributedWorks { get; set; } = new()
        {
            new() { Title = "Primary Audiobook Class 1 (Alphabet Sounds)", Category = "Audiobook Production", Date = "2026-10-15", Status = "Published" },
            new() { Title = "Inclusive Primary School Empathy Session - Gazipur", Category = "Offline Workshop", Date = "2026-10-22", Status = "Published" }
        };

        private string NewWorkTitle { get; set; } = "";
        private string NewWorkCategory { get; set; } = "Audiobook Production";
        private string NewWorkYouTubeUrl { get; set; } = "";
        private string NewWorkDescription { get; set; } = "";

        private bool ShowAlert { get; set; } = false;
        private string AlertMessage { get; set; } = "";

        protected override void OnInitialized()
        {
            AppState.OnChange += StateHasChanged;
        }

        private string GetInitials(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return "ME";
            var parts = name.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 1) return parts[0][0].ToString().ToUpper();
            return (parts[0][0].ToString() + parts[^1][0].ToString()).ToUpper();
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

        private async Task HandleSaveProfile()
        {
            await SupabaseService.UpdateProfileAsync(ProfileModel);
            TriggerAlert($"✓ Profile changes for {ProfileModel.FullName} saved successfully!");
        }

        private void SubmitNewWork()
        {
            if (string.IsNullOrWhiteSpace(NewWorkTitle))
            {
                TriggerAlert("⚠️ Please enter a project title.");
                return;
            }

            ContributedWorks.Insert(0, new ContributedWorkItem
            {
                Title = NewWorkTitle.Trim(),
                Category = NewWorkCategory,
                Date = DateTime.Now.ToString("yyyy-MM-dd"),
                Status = "Pending Review"
            });

            TriggerAlert($"✓ Successfully submitted '{NewWorkTitle}' for staff verification!");
            NewWorkTitle = "";
            NewWorkDescription = "";
            ActiveTab = "showcase";
        }

        private void DeleteWork(ContributedWorkItem item)
        {
            ContributedWorks.Remove(item);
            TriggerAlert($"✓ Removed contribution '{item.Title}'.");
        }

        public void Dispose()
        {
            AppState.OnChange -= StateHasChanged;
        }
    }
}

