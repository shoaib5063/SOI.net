using Microsoft.AspNetCore.Components;

namespace StoriesOfInclusion.Components.Pages
{
    public partial class Team : ComponentBase, IDisposable
    {
        private record TeamMember(string Initials, string Name, string Role, string Bio);

        private List<TeamMember> Members { get; set; } = new()
        {
            new("IN", "Iffat Nusaiba", "Co-Founder", "Executive leadership, project oversight, and inclusive curriculum direction."),
            new("ME", "Musarrat Jahan Esha", "Creative Lead & Co-Founder", "Creative direction, visual storytelling, and accessibility design strategy."),
            new("NT", "Naima Jannat Taki", "Program Lead & Co-Founder", "Program design, school outreach implementation, and stakeholder relations."),
            new("RA", "Md. Rafieu Hossain Arnob", "Operations Strategist", "Operational efficiency, resource allocation, and program scalability."),
            new("MT", "Masruba Mehejabin Tanha", "Public Engagement & Content Coordinator", "Public outreach, community engagement, and digital awareness series."),
            new("EC", "Ehsan Al Rakib Chowdhury", "Campaign & Outreach Coordinator", "School campaign execution, volunteer management, and offline logistics."),
            new("KS", "Kazi Airin Sultana", "Design & Branding Coordinator", "Accessible branding, educational illustrations, and campaign identity."),
            new("AO", "Afnan Jahan Orthey", "Co-Founder", "Co-founding strategic direction and policy advocacy partnerships."),
            new("IP", "Ismail Hosen Polash", "Media & Production Coordinator", "Audiobook studio recording, audio mastering, and video production.")
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
