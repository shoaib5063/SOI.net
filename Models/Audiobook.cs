using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoriesOfInclusion.Models
{
    [Table("audiobooks")]
    public class Audiobook
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("title_en")]
        public string TitleEn { get; set; } = string.Empty;

        [Required]
        [Column("title_bn")]
        public string TitleBn { get; set; } = string.Empty;

        [Required]
        [Column("grade_level")]
        public string GradeLevel { get; set; } = "Class 1";

        [Required]
        [Column("youtube_url")]
        public string YouTubeUrl { get; set; } = string.Empty;

        [Column("audio_url")]
        public string? AudioUrl { get; set; }

        [Column("transcript_en")]
        public string? TranscriptEn { get; set; }

        [Column("transcript_bn")]
        public string? TranscriptBn { get; set; }

        [Column("duration_seconds")]
        public int DurationSeconds { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
