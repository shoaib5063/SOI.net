namespace StoriesOfInclusion.Models
{
    public class MagazineIssue
    {
        public int VolumeNumber { get; set; }
        public int IssueNumber { get; set; }
        public string TitleEn { get; set; } = string.Empty;
        public string TitleBn { get; set; } = string.Empty;
        public string ThemeEn { get; set; } = string.Empty;
        public string ThemeBn { get; set; } = string.Empty;
        public string CoverImageUrl { get; set; } = string.Empty;
        public string PublishedDate { get; set; } = string.Empty;
        public string EditorNoteEn { get; set; } = string.Empty;
        public string EditorNoteBn { get; set; } = string.Empty;
        public List<MagazineArticle> Articles { get; set; } = new();
    }

    public class MagazineArticle
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public int VolumeNumber { get; set; }
        public int IssueNumber { get; set; }
        public string TitleEn { get; set; } = string.Empty;
        public string TitleBn { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string AuthorRole { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string ReadTimeMinutes { get; set; } = "5 min";
        public string ExcerptEn { get; set; } = string.Empty;
        public string ExcerptBn { get; set; } = string.Empty;
        public string ContentEn { get; set; } = string.Empty;
        public string ContentBn { get; set; } = string.Empty;
        public string AudioSummaryUrl { get; set; } = string.Empty;
        public bool HasAudioNarration { get; set; } = true;
    }
}

