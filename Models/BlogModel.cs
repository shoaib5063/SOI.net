namespace StoriesOfInclusion.Models
{
    public class BlogPost
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string TitleEn { get; set; } = string.Empty;
        public string TitleBn { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string AuthorRole { get; set; } = string.Empty;
        public string PublishedDate { get; set; } = string.Empty;
        public string ReadTimeMinutes { get; set; } = "4 min";
        public string ExcerptEn { get; set; } = string.Empty;
        public string ExcerptBn { get; set; } = string.Empty;
        public string ContentEn { get; set; } = string.Empty;
        public string ContentBn { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string ImageCaptionEn { get; set; } = string.Empty;
        public string ImageCaptionBn { get; set; } = string.Empty;
        public string AudioTranscriptEn { get; set; } = string.Empty;
        public string AudioTranscriptBn { get; set; } = string.Empty;
        public List<string> Tags { get; set; } = new();
    }
}

