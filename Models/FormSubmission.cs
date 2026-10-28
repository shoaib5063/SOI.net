using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoriesOfInclusion.Models
{
    [Table("form_submissions")]
    public class FormSubmission
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("submission_type")]
        public string SubmissionType { get; set; } = "Join Us"; // "Join Us" or "Contact Inquiry"

        [Required]
        [Column("full_name")]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Column("phone")]
        public string? Phone { get; set; }

        [Required]
        [Column("message")]
        public string Message { get; set; } = string.Empty;

        [Column("status")]
        public string Status { get; set; } = "New";

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
