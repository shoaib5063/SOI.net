namespace StoriesOfInclusion.Models
{
    public class GalleryItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string TitleEn { get; set; } = string.Empty;
        public string TitleBn { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string AltTextEn { get; set; } = string.Empty;
        public string AltTextBn { get; set; } = string.Empty;
        public string LocationDateEn { get; set; } = string.Empty;
        public string LocationDateBn { get; set; } = string.Empty;
        public string DescriptionEn { get; set; } = string.Empty;
        public string DescriptionBn { get; set; } = string.Empty;
    }
}

