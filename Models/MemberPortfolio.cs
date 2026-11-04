namespace StoriesOfInclusion.Models
{
    public class PortfolioWorkItem
    {
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = "Audiobook";
        public string Year { get; set; } = "2026";
        public string Summary { get; set; } = string.Empty;
        public string? LinkUrl { get; set; }
        public string ImpactMetric { get; set; } = string.Empty;
    }

    public class MemberPortfolio
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Slug { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Initials { get; set; } = string.Empty;
        public string RoleTitle { get; set; } = string.Empty;
        public string SystemRole { get; set; } = "user"; // "user", "admin", "superadmin"
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Location { get; set; } = "Dhaka, Bangladesh";
        public int JoinedYear { get; set; } = 2024;
        public bool IsVerified { get; set; } = true;
        public string Quote { get; set; } = string.Empty;
        public string BioEn { get; set; } = string.Empty;
        public string BioBn { get; set; } = string.Empty;
        public List<string> Specialties { get; set; } = new();
        public List<string> ImpactHighlights { get; set; } = new();
        public List<PortfolioWorkItem> ContributedWorks { get; set; } = new();
    }
}
