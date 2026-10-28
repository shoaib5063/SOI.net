using Microsoft.AspNetCore.Components;

namespace StoriesOfInclusion.Components.Pages
{
    public partial class About : ComponentBase, IDisposable
    {
        private record PillarItem(string Icon, string Title, string Description);

        private List<PillarItem> Pillars { get; set; } = new()
        {
            new("📢", "Raise Awareness, Expand Reach & Impact", "Spread knowledge about the importance of inclusive education and the daily challenges faced by students with disabilities."),
            new("📚", "Create Accessible Learning Resources", "Develop audiobooks, sign language videos, and multisensory tools to support students with diverse learning needs."),
            new("🏫", "Engage Schools & Teachers", "Conduct interactive training sessions and discussions to help educators build welcoming and accessible classrooms."),
            new("🤝", "Support Students & Families", "Work closely with students with disabilities and their families to ensure they feel valued, safe, and empowered."),
            new("⚖️", "Advocate for Policy Changes", "Encourage schools and educational policymakers to implement inclusive policies and curriculum adaptations."),
            new("💡", "Promote Multiple Intelligence Learning", "Recognize that every child learns differently and provide varied sensory methods to help all students thrive.")
        };

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
