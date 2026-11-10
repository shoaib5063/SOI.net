using StoriesOfInclusion.Models;

namespace StoriesOfInclusion.Services
{
    public interface ISupabaseService
    {
        Task<List<Audiobook>> GetAudiobooksAsync();
        Task<bool> SubmitFormAsync(FormSubmission submission);
        Task<List<FormSubmission>> GetSubmissionsAsync();
        Task<bool> UpdateSubmissionStatusAsync(int id, string status);
        Task<bool> DeleteSubmissionAsync(int id);
        Task<Profile?> GetProfileAsync(Guid userId);
        Task<bool> UpdateProfileAsync(Profile profile);
        Task<List<Profile>> GetAllUsersAsync();
        Task<bool> UpdateUserRoleAsync(Guid userId, string newRole);
    }

    public class SupabaseService : ISupabaseService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<SupabaseService> _logger;

        private static int _nextSubmissionId = 4;
        private static readonly List<FormSubmission> _formSubmissions = new()
        {
            new()
            {
                Id = 1,
                SubmissionType = "Join Us",
                FullName = "Tanvir Ahmed",
                Email = "tanvir.audio@volunteer.org",
                Phone = "+880 1711-223344",
                Message = "Interested in volunteering for primary audiobook voice recording in Bengali. I have prior audio storytelling experience.",
                Status = "New",
                CreatedAt = DateTime.UtcNow.AddHours(-3)
            },
            new()
            {
                Id = 2,
                SubmissionType = "Contact Inquiry",
                FullName = "Rahima Khatun",
                Email = "rahima@gazipur-primary.edu.bd",
                Phone = "+880 1819-988776",
                Message = "We would love to implement the inclusive audiobook program in our primary school in Gazipur accommodating 12 visually impaired children.",
                Status = "Under Review",
                CreatedAt = DateTime.UtcNow.AddDays(-1)
            },
            new()
            {
                Id = 3,
                SubmissionType = "Join Us",
                FullName = "Naimur Rahman",
                Email = "naimur.design@inclusion.bd",
                Phone = "+880 1912-345678",
                Message = "Accessible graphic designer and illustrator ready to assist with high-contrast tactile storybook characters.",
                Status = "Contacted",
                CreatedAt = DateTime.UtcNow.AddDays(-2)
            }
        };

        private static readonly List<Profile> TeamProfiles = new()
        {
            new() { Id = Guid.NewGuid(), FullName = "Iffat Nusaiba", Email = "nusaibaiffaat3@gmail.com", Role = "superadmin", BioEn = "Executive leadership, project oversight, and inclusive curriculum direction.", IsPortfolioApproved = true },
            new() { Id = Guid.NewGuid(), FullName = "Musarrat Jahan Esha", Email = "mjesha12@gmail.com", Role = "admin", BioEn = "Creative direction, visual storytelling, and accessibility design strategy.", IsPortfolioApproved = true },
            new() { Id = Guid.NewGuid(), FullName = "Naima Jannat Taki", Email = "naima.taki@storiesofinclusion.org", Role = "admin", BioEn = "Program design, school outreach implementation, and stakeholder relations.", IsPortfolioApproved = true },
            new() { Id = Guid.NewGuid(), FullName = "Md. Rafieu Hossain Arnob", Email = "rafieu.arnob@storiesofinclusion.org", Role = "admin", BioEn = "Operational efficiency, resource allocation, and program scalability.", IsPortfolioApproved = true },
            new() { Id = Guid.NewGuid(), FullName = "Afnan Jahan Orthey", Email = "afnan.orthey@storiesofinclusion.org", Role = "admin", BioEn = "Co-founding strategic direction and policy advocacy partnerships.", IsPortfolioApproved = true },
            new() { Id = Guid.NewGuid(), FullName = "Masruba Mehejabin Tanha", Email = "masruba.tanha@storiesofinclusion.org", Role = "user", BioEn = "Public outreach, community engagement, and digital awareness series.", IsPortfolioApproved = true },
            new() { Id = Guid.NewGuid(), FullName = "Ehsan Al Rakib Chowdhury", Email = "ehsan.rakib@storiesofinclusion.org", Role = "user", BioEn = "School campaign execution, volunteer management, and offline logistics.", IsPortfolioApproved = true },
            new() { Id = Guid.NewGuid(), FullName = "Kazi Airin Sultana", Email = "kazi.airin@storiesofinclusion.org", Role = "user", BioEn = "Accessible branding, educational illustrations, and campaign identity.", IsPortfolioApproved = true },
            new() { Id = Guid.NewGuid(), FullName = "Ismail Hosen Polash", Email = "ismail.polash@storiesofinclusion.org", Role = "user", BioEn = "Audiobook studio recording, audio mastering, and video production.", IsPortfolioApproved = true }
        };

        private static readonly List<Audiobook> _audiobooks = new()
        {
            new()
            {
                Id = 1,
                GradeLevel = "Class 1",
                TitleBn = @"১ম শ্রেণি, আমার বাংলা বই, পাঠ - ১০ থেকে ১৬ (বর্ণ শিখি) | ১ম পর্ব | অ থেকে ঊ",
                TitleEn = @"Class 1 | Amar Bangla Boi | পাঠ - ১০ থেকে ১৬ (বর্ণ শিখি) | ১ম পর্ব | অ থেকে ঊ",
                YouTubeUrl = "https://www.youtube.com/watch?v=PUCU1FURW7k",
                DurationSeconds = 185,
                TranscriptBn = @"দৃষ্টিপ্রতিবন্ধী শিক্ষার্থীদের জন্য প্রথম শ্রেণির বাংলা পাঠ্যবইয়ের সহজ ও স্পষ্ট বর্ণশিক্ষা। সঠিক উচ্চারণ ও ধ্বনির সমন্বয়ে তৈরি অডিও পাঠ।",
                TranscriptEn = @"Clear, accessible phonics and alphabet learning audio lesson for Class 1 primary students, carefully voiced for visually challenged and early learners."
            },
            new()
            {
                Id = 2,
                GradeLevel = "Class 1",
                TitleBn = @"১ম শ্রেণি, আমার বাংলা বই, পাঠ - ১৯, ২০ (বর্ণ শিখি) | ব্যঞ্জনবর্ণ | ১ম পর্ব | ক থেকে ঞ",
                TitleEn = @"Class 1 | Amar Bangla Boi | পাঠ - ১৯, ২০ (বর্ণ শিখি) | ব্যঞ্জনবর্ণ | ১ম পর্ব | ক থেকে ঞ",
                YouTubeUrl = "https://www.youtube.com/watch?v=N1LM6uDG3pw",
                DurationSeconds = 195,
                TranscriptBn = @"দৃষ্টিপ্রতিবন্ধী শিক্ষার্থীদের জন্য প্রথম শ্রেণির বাংলা পাঠ্যবইয়ের সহজ ও স্পষ্ট বর্ণশিক্ষা। সঠিক উচ্চারণ ও ধ্বনির সমন্বয়ে তৈরি অডিও পাঠ।",
                TranscriptEn = @"Clear, accessible phonics and alphabet learning audio lesson for Class 1 primary students, carefully voiced for visually challenged and early learners."
            },
            new()
            {
                Id = 3,
                GradeLevel = "Class 1",
                TitleBn = @"১ম শ্রেণি, আমার বাংলা বই, পাঠ - ২৩, ২৪ (বর্ণ শিখি) | ব্যঞ্জনবর্ণ | ২য় পর্ব | ট থেকে ন",
                TitleEn = @"Class 1 | Amar Bangla Boi | পাঠ - ২৩, ২৪ (বর্ণ শিখি) | ব্যঞ্জনবর্ণ | ২য় পর্ব | ট থেকে ন",
                YouTubeUrl = "https://www.youtube.com/watch?v=mwhRWKfLxHs",
                DurationSeconds = 193,
                TranscriptBn = @"দৃষ্টিপ্রতিবন্ধী শিক্ষার্থীদের জন্য প্রথম শ্রেণির বাংলা পাঠ্যবইয়ের সহজ ও স্পষ্ট বর্ণশিক্ষা। সঠিক উচ্চারণ ও ধ্বনির সমন্বয়ে তৈরি অডিও পাঠ।",
                TranscriptEn = @"Clear, accessible phonics and alphabet learning audio lesson for Class 1 primary students, carefully voiced for visually challenged and early learners."
            },
            new()
            {
                Id = 4,
                GradeLevel = "Class 1",
                TitleBn = @"১ম শ্রেণি, আমার বাংলা বই, পাঠ - ২৬, ৩২ (বর্ণ শিখি) | ব্যঞ্জনবর্ণ | ৩য় পর্ব | প থেকে ষ",
                TitleEn = @"Class 1 | Amar Bangla Boi | পাঠ - ২৬, ৩২ (বর্ণ শিখি) | ব্যঞ্জনবর্ণ | ৩য় পর্ব | প থেকে ষ",
                YouTubeUrl = "https://www.youtube.com/watch?v=i2SK4VcvnU4",
                DurationSeconds = 188,
                TranscriptBn = @"দৃষ্টিপ্রতিবন্ধী শিক্ষার্থীদের জন্য প্রথম শ্রেণির বাংলা পাঠ্যবইয়ের সহজ ও স্পষ্ট বর্ণশিক্ষা। সঠিক উচ্চারণ ও ধ্বনির সমন্বয়ে তৈরি অডিও পাঠ।",
                TranscriptEn = @"Clear, accessible phonics and alphabet learning audio lesson for Class 1 primary students, carefully voiced for visually challenged and early learners."
            },
            new()
            {
                Id = 5,
                GradeLevel = "Class 1",
                TitleBn = @"১ম শ্রেণি, আমার বাংলা বই, পাঠ - ৩৪ ৩৯ (বর্ণ শিখি) | ব্যঞ্জনবর্ণ | ৪র্থ পর্ব | স থেকে চন্দ্রবিন্দু",
                TitleEn = @"Class 1 | Amar Bangla Boi | পাঠ - ৩৪ ৩৯ (বর্ণ শিখি) | ব্যঞ্জনবর্ণ | ৪র্থ পর্ব | স থেকে চন্দ্রবিন্দু",
                YouTubeUrl = "https://www.youtube.com/watch?v=luIqWumEQ-c",
                DurationSeconds = 169,
                TranscriptBn = @"দৃষ্টিপ্রতিবন্ধী শিক্ষার্থীদের জন্য প্রথম শ্রেণির বাংলা পাঠ্যবইয়ের সহজ ও স্পষ্ট বর্ণশিক্ষা। সঠিক উচ্চারণ ও ধ্বনির সমন্বয়ে তৈরি অডিও পাঠ।",
                TranscriptEn = @"Clear, accessible phonics and alphabet learning audio lesson for Class 1 primary students, carefully voiced for visually challenged and early learners."
            },
            new()
            {
                Id = 6,
                GradeLevel = "Class 2",
                TitleBn = @"২য় শ্রেণি, আমার বাংলা বই, পাঠ - ৬ | আয় দেখে যা নাচ",
                TitleEn = @"Class 2 | Amar Bangla Boi | পাঠ - ৬ | আয় দেখে যা নাচ",
                YouTubeUrl = "https://www.youtube.com/watch?v=jbAp8rDZDnk",
                DurationSeconds = 119,
                TranscriptBn = @"দ্বিতীয় শ্রেণির শিক্ষার্থীদের জন্য সহজ ও সাবলীল ভাষায় রচিত সাহিত্য ও নীতিশিক্ষামূলক পাঠ। শ্রবণভিত্তিক বিশেষ পদ্ধতিতে প্রস্তুতকৃত।",
                TranscriptEn = @"Engaging primary school literature, values, and narrative lesson adapted for Class 2 students with visual impairments and assistive learning needs."
            },
            new()
            {
                Id = 7,
                GradeLevel = "Class 2",
                TitleBn = @"২য় শ্রেণি, আমার বাংলা বই, পাঠ -১০ | একুশের গান",
                TitleEn = @"Class 2 | Amar Bangla Boi | পাঠ -১০ | একুশের গান",
                YouTubeUrl = "https://www.youtube.com/watch?v=cgpBgnt0s9U",
                DurationSeconds = 137,
                TranscriptBn = @"দ্বিতীয় শ্রেণির শিক্ষার্থীদের জন্য সহজ ও সাবলীল ভাষায় রচিত সাহিত্য ও নীতিশিক্ষামূলক পাঠ। শ্রবণভিত্তিক বিশেষ পদ্ধতিতে প্রস্তুতকৃত।",
                TranscriptEn = @"Engaging primary school literature, values, and narrative lesson adapted for Class 2 students with visual impairments and assistive learning needs."
            },
            new()
            {
                Id = 8,
                GradeLevel = "Class 2",
                TitleBn = @"২য় শ্রেণি, আমার বাংলা বই, পাঠ -৪ | ডালিমকুমার ও কঙ্কাবতী",
                TitleEn = @"Class 2 | Amar Bangla Boi | পাঠ -৪ | ডালিমকুমার ও কঙ্কাবতী",
                YouTubeUrl = "https://www.youtube.com/watch?v=n06hpRj_Cgc",
                DurationSeconds = 111,
                TranscriptBn = @"দ্বিতীয় শ্রেণির শিক্ষার্থীদের জন্য সহজ ও সাবলীল ভাষায় রচিত সাহিত্য ও নীতিশিক্ষামূলক পাঠ। শ্রবণভিত্তিক বিশেষ পদ্ধতিতে প্রস্তুতকৃত।",
                TranscriptEn = @"Engaging primary school literature, values, and narrative lesson adapted for Class 2 students with visual impairments and assistive learning needs."
            },
            new()
            {
                Id = 9,
                GradeLevel = "Class 2",
                TitleBn = @"২য় শ্রেণি, আমার বাংলা বই, পাঠ - ১৪ | কাজের আনন্দ",
                TitleEn = @"Class 2 | Amar Bangla Boi | পাঠ - ১৪ | কাজের আনন্দ",
                YouTubeUrl = "https://www.youtube.com/watch?v=7XW9S_uLJMY",
                DurationSeconds = 166,
                TranscriptBn = @"দ্বিতীয় শ্রেণির শিক্ষার্থীদের জন্য সহজ ও সাবলীল ভাষায় রচিত সাহিত্য ও নীতিশিক্ষামূলক পাঠ। শ্রবণভিত্তিক বিশেষ পদ্ধতিতে প্রস্তুতকৃত।",
                TranscriptEn = @"Engaging primary school literature, values, and narrative lesson adapted for Class 2 students with visual impairments and assistive learning needs."
            },
            new()
            {
                Id = 10,
                GradeLevel = "Class 2",
                TitleBn = @"২য় শ্রেণি, আমার বাংলা বই, পাঠ - ১৭ | গ্রাম ও শহর",
                TitleEn = @"Class 2 | Amar Bangla Boi | পাঠ - ১৭ | গ্রাম ও শহর",
                YouTubeUrl = "https://www.youtube.com/watch?v=IXhvcxYo2cQ",
                DurationSeconds = 136,
                TranscriptBn = @"দ্বিতীয় শ্রেণির শিক্ষার্থীদের জন্য সহজ ও সাবলীল ভাষায় রচিত সাহিত্য ও নীতিশিক্ষামূলক পাঠ। শ্রবণভিত্তিক বিশেষ পদ্ধতিতে প্রস্তুতকৃত।",
                TranscriptEn = @"Engaging primary school literature, values, and narrative lesson adapted for Class 2 students with visual impairments and assistive learning needs."
            },
            new()
            {
                Id = 11,
                GradeLevel = "Class 2",
                TitleBn = @"২য় শ্রেণি, আমার বাংলা বই, পাঠ - ১৯ | বিড়াল ছানা",
                TitleEn = @"Class 2 | Amar Bangla Boi | পাঠ - ১৯ | বিড়াল ছানা",
                YouTubeUrl = "https://www.youtube.com/watch?v=LFK6-9QtMPw",
                DurationSeconds = 176,
                TranscriptBn = @"দ্বিতীয় শ্রেণির শিক্ষার্থীদের জন্য সহজ ও সাবলীল ভাষায় রচিত সাহিত্য ও নীতিশিক্ষামূলক পাঠ। শ্রবণভিত্তিক বিশেষ পদ্ধতিতে প্রস্তুতকৃত।",
                TranscriptEn = @"Engaging primary school literature, values, and narrative lesson adapted for Class 2 students with visual impairments and assistive learning needs."
            },
            new()
            {
                Id = 12,
                GradeLevel = "Class 2",
                TitleBn = @"২য় শ্রেণি, আমার বাংলা বই, পাঠ - ২৫ | মুক্তির সেনা",
                TitleEn = @"Class 2 | Amar Bangla Boi | পাঠ - ২৫ | মুক্তির সেনা",
                YouTubeUrl = "https://www.youtube.com/watch?v=44tBw9m7hOc",
                DurationSeconds = 161,
                TranscriptBn = @"দ্বিতীয় শ্রেণির শিক্ষার্থীদের জন্য সহজ ও সাবলীল ভাষায় রচিত সাহিত্য ও নীতিশিক্ষামূলক পাঠ। শ্রবণভিত্তিক বিশেষ পদ্ধতিতে প্রস্তুতকৃত।",
                TranscriptEn = @"Engaging primary school literature, values, and narrative lesson adapted for Class 2 students with visual impairments and assistive learning needs."
            },
            new()
            {
                Id = 13,
                GradeLevel = "Class 2",
                TitleBn = @"২য় শ্রেণি, আমার বাংলা বই, পাঠ ২২ | আমাদের ছোট নদী",
                TitleEn = @"Class 2 | Amar Bangla Boi | পাঠ ২২ | আমাদের ছোট নদী",
                YouTubeUrl = "https://www.youtube.com/watch?v=g_VXWbFIcEI",
                DurationSeconds = 178,
                TranscriptBn = @"দ্বিতীয় শ্রেণির শিক্ষার্থীদের জন্য সহজ ও সাবলীল ভাষায় রচিত সাহিত্য ও নীতিশিক্ষামূলক পাঠ। শ্রবণভিত্তিক বিশেষ পদ্ধতিতে প্রস্তুতকৃত।",
                TranscriptEn = @"Engaging primary school literature, values, and narrative lesson adapted for Class 2 students with visual impairments and assistive learning needs."
            },
            new()
            {
                Id = 14,
                GradeLevel = "Class 2",
                TitleBn = @"২য় শ্রেণি, আমার বাংলা বই, পাঠ ২১ | পয়লা বৈশাখ",
                TitleEn = @"Class 2 | Amar Bangla Boi | পাঠ ২১ | পয়লা বৈশাখ",
                YouTubeUrl = "https://www.youtube.com/watch?v=e3K90yo7y78",
                DurationSeconds = 141,
                TranscriptBn = @"দ্বিতীয় শ্রেণির শিক্ষার্থীদের জন্য সহজ ও সাবলীল ভাষায় রচিত সাহিত্য ও নীতিশিক্ষামূলক পাঠ। শ্রবণভিত্তিক বিশেষ পদ্ধতিতে প্রস্তুতকৃত।",
                TranscriptEn = @"Engaging primary school literature, values, and narrative lesson adapted for Class 2 students with visual impairments and assistive learning needs."
            },
            new()
            {
                Id = 15,
                GradeLevel = "Class 3",
                TitleBn = @"আমার বাংলা বই | ৩য় শ্রেণি । পাঠ ৩",
                TitleEn = @"Class 3 | Amar Bangla Boi | পাঠ ৩",
                YouTubeUrl = "https://www.youtube.com/watch?v=4Ut7K_m0TYQ",
                DurationSeconds = 190,
                TranscriptBn = @"তৃতীয় শ্রেণির পাঠ্যপুস্তকের সাহিত্য পাঠ। প্রাকৃতিক সৌন্দর্য, মানবিক মূল্যবোধ ও বর্ণনামূলক সাহিত্যের সহজ ব্যাখ্যা।",
                TranscriptEn = @"Accessible Class 3 textbook literature lesson exploring Bangladesh's nature, cultural heritage, and moral insights designed for auditory comprehension."
            },
            new()
            {
                Id = 16,
                GradeLevel = "Class 3",
                TitleBn = @"আমার বাংলা বই, তৃতীয় শ্রেণি, পাঠ ১২।",
                TitleEn = @"Class 3 | Amar Bangla Boi | পাঠ ১২।",
                YouTubeUrl = "https://www.youtube.com/watch?v=T2D6Dxm7j6Y",
                DurationSeconds = 105,
                TranscriptBn = @"তৃতীয় শ্রেণির পাঠ্যপুস্তকের সাহিত্য পাঠ। প্রাকৃতিক সৌন্দর্য, মানবিক মূল্যবোধ ও বর্ণনামূলক সাহিত্যের সহজ ব্যাখ্যা।",
                TranscriptEn = @"Accessible Class 3 textbook literature lesson exploring Bangladesh's nature, cultural heritage, and moral insights designed for auditory comprehension."
            },
            new()
            {
                Id = 17,
                GradeLevel = "Class 3",
                TitleBn = @"আমার বাংলা বই, তৃতীয় শ্রেণি, পাঠ ৭।",
                TitleEn = @"Class 3 | Amar Bangla Boi | পাঠ ৭।",
                YouTubeUrl = "https://www.youtube.com/watch?v=dij4IAuxivE",
                DurationSeconds = 620,
                TranscriptBn = @"তৃতীয় শ্রেণির পাঠ্যপুস্তকের সাহিত্য পাঠ। প্রাকৃতিক সৌন্দর্য, মানবিক মূল্যবোধ ও বর্ণনামূলক সাহিত্যের সহজ ব্যাখ্যা।",
                TranscriptEn = @"Accessible Class 3 textbook literature lesson exploring Bangladesh's nature, cultural heritage, and moral insights designed for auditory comprehension."
            },
            new()
            {
                Id = 18,
                GradeLevel = "Class 3",
                TitleBn = @"আমার বাংলা বই, তৃতীয় শ্রেণি, পাঠ ১১।",
                TitleEn = @"Class 3 | Amar Bangla Boi | পাঠ ১১।",
                YouTubeUrl = "https://www.youtube.com/watch?v=msw6SCOi-M0",
                DurationSeconds = 159,
                TranscriptBn = @"তৃতীয় শ্রেণির পাঠ্যপুস্তকের সাহিত্য পাঠ। প্রাকৃতিক সৌন্দর্য, মানবিক মূল্যবোধ ও বর্ণনামূলক সাহিত্যের সহজ ব্যাখ্যা।",
                TranscriptEn = @"Accessible Class 3 textbook literature lesson exploring Bangladesh's nature, cultural heritage, and moral insights designed for auditory comprehension."
            },
            new()
            {
                Id = 19,
                GradeLevel = "Class 3",
                TitleBn = @"আমার বাংলা বই, তৃতীয় শ্রেণি, পাঠ ৮।",
                TitleEn = @"Class 3 | Amar Bangla Boi | পাঠ ৮।",
                YouTubeUrl = "https://www.youtube.com/watch?v=xU_YwTn3B9k",
                DurationSeconds = 419,
                TranscriptBn = @"তৃতীয় শ্রেণির পাঠ্যপুস্তকের সাহিত্য পাঠ। প্রাকৃতিক সৌন্দর্য, মানবিক মূল্যবোধ ও বর্ণনামূলক সাহিত্যের সহজ ব্যাখ্যা।",
                TranscriptEn = @"Accessible Class 3 textbook literature lesson exploring Bangladesh's nature, cultural heritage, and moral insights designed for auditory comprehension."
            },
            new()
            {
                Id = 20,
                GradeLevel = "Class 3",
                TitleBn = @"আমার বাংলা বই, তৃতীয় শ্রেণি, পাঠ ১।",
                TitleEn = @"Class 3 | Amar Bangla Boi | পাঠ ১।",
                YouTubeUrl = "https://www.youtube.com/watch?v=gKbM9uX8zoM",
                DurationSeconds = 63,
                TranscriptBn = @"তৃতীয় শ্রেণির পাঠ্যপুস্তকের সাহিত্য পাঠ। প্রাকৃতিক সৌন্দর্য, মানবিক মূল্যবোধ ও বর্ণনামূলক সাহিত্যের সহজ ব্যাখ্যা।",
                TranscriptEn = @"Accessible Class 3 textbook literature lesson exploring Bangladesh's nature, cultural heritage, and moral insights designed for auditory comprehension."
            },
            new()
            {
                Id = 21,
                GradeLevel = "Class 3",
                TitleBn = @"Class 3 | English For Today | Unit 39 | Story : The Crow | Lessons 1-9",
                TitleEn = @"Class 3 | English For Today | Unit 39: The Crow (Lessons 1-9)",
                YouTubeUrl = "https://www.youtube.com/watch?v=pT8YG_krDf0",
                DurationSeconds = 117,
                TranscriptBn = @"তৃতীয় শ্রেণির ইংরেজি পাঠ: তৃষ্ণার্ত কাকের গল্প। বুদ্ধি ও ধৈর্যের মাধ্যমে কীভাবে সমস্যা সমাধান করা যায়, তা সুন্দর ইংরেজি উচ্চারণে বর্ণিত।",
                TranscriptEn = @"An engaging English fable lesson for Class 3 teaching patience, critical problem-solving, and vocabulary building through the story of the thirsty crow."
            },
            new()
            {
                Id = 22,
                GradeLevel = "Class 4",
                TitleBn = @"চতুর্থ শ্রেনি | পাঠ ১৭| আমার বাংলা বই",
                TitleEn = @"Class 4 | Amar Bangla Boi | পাঠ ১৭| আমার বাংলা বই",
                YouTubeUrl = "https://www.youtube.com/watch?v=GpT7DoZZlvA",
                DurationSeconds = 171,
                TranscriptBn = @"চতুর্থ শ্রেণির শিক্ষার্থীদের উপযোগী বাংলা পাঠ্যসূচির সম্পূর্ণ অডিও সংস্করণ। পাঠ্যক্রমের প্রতিটি শব্দ স্পষ্ট ও সহজবোধ্যভাবে উপস্থাপিত।",
                TranscriptEn = @"Comprehensive audio textbook lesson for Class 4 primary curriculum, mastered specifically for inclusive classrooms and blind learners."
            },
            new()
            {
                Id = 23,
                GradeLevel = "Class 4",
                TitleBn = @"চতুর্থ শ্রেনি | পাঠ ১৫ | আমার বাংলা বই",
                TitleEn = @"Class 4 | Amar Bangla Boi | পাঠ ১৫ | আমার বাংলা বই",
                YouTubeUrl = "https://www.youtube.com/watch?v=-Lyo0Ksk8z0",
                DurationSeconds = 313,
                TranscriptBn = @"চতুর্থ শ্রেণির শিক্ষার্থীদের উপযোগী বাংলা পাঠ্যসূচির সম্পূর্ণ অডিও সংস্করণ। পাঠ্যক্রমের প্রতিটি শব্দ স্পষ্ট ও সহজবোধ্যভাবে উপস্থাপিত।",
                TranscriptEn = @"Comprehensive audio textbook lesson for Class 4 primary curriculum, mastered specifically for inclusive classrooms and blind learners."
            },
            new()
            {
                Id = 24,
                GradeLevel = "Class 4",
                TitleBn = @"চতুর্থ শ্রেনি | পাঠ ১৪ | আমার বাংলা বই",
                TitleEn = @"Class 4 | Amar Bangla Boi | পাঠ ১৪ | আমার বাংলা বই",
                YouTubeUrl = "https://www.youtube.com/watch?v=z9cS6_Mh5xc",
                DurationSeconds = 124,
                TranscriptBn = @"চতুর্থ শ্রেণির শিক্ষার্থীদের উপযোগী বাংলা পাঠ্যসূচির সম্পূর্ণ অডিও সংস্করণ। পাঠ্যক্রমের প্রতিটি শব্দ স্পষ্ট ও সহজবোধ্যভাবে উপস্থাপিত।",
                TranscriptEn = @"Comprehensive audio textbook lesson for Class 4 primary curriculum, mastered specifically for inclusive classrooms and blind learners."
            },
            new()
            {
                Id = 25,
                GradeLevel = "Class 4",
                TitleBn = @"চতুর্থ শ্রেনি | পাঠ ১৩ | আমার বাংলা বই",
                TitleEn = @"Class 4 | Amar Bangla Boi | পাঠ ১৩ | আমার বাংলা বই",
                YouTubeUrl = "https://www.youtube.com/watch?v=YZWnFprTedY",
                DurationSeconds = 307,
                TranscriptBn = @"চতুর্থ শ্রেণির শিক্ষার্থীদের উপযোগী বাংলা পাঠ্যসূচির সম্পূর্ণ অডিও সংস্করণ। পাঠ্যক্রমের প্রতিটি শব্দ স্পষ্ট ও সহজবোধ্যভাবে উপস্থাপিত।",
                TranscriptEn = @"Comprehensive audio textbook lesson for Class 4 primary curriculum, mastered specifically for inclusive classrooms and blind learners."
            },
            new()
            {
                Id = 26,
                GradeLevel = "Class 4",
                TitleBn = @"চতুর্থ শ্রেনি | পাঠ ১২ | আমার বাংলা বই",
                TitleEn = @"Class 4 | Amar Bangla Boi | পাঠ ১২ | আমার বাংলা বই",
                YouTubeUrl = "https://www.youtube.com/watch?v=utQLmM_RJn4",
                DurationSeconds = 214,
                TranscriptBn = @"চতুর্থ শ্রেণির শিক্ষার্থীদের উপযোগী বাংলা পাঠ্যসূচির সম্পূর্ণ অডিও সংস্করণ। পাঠ্যক্রমের প্রতিটি শব্দ স্পষ্ট ও সহজবোধ্যভাবে উপস্থাপিত।",
                TranscriptEn = @"Comprehensive audio textbook lesson for Class 4 primary curriculum, mastered specifically for inclusive classrooms and blind learners."
            },
            new()
            {
                Id = 27,
                GradeLevel = "Class 4",
                TitleBn = @"চতুর্থ শ্রেনি | পাঠ ১১ | আমার বাংলা বই",
                TitleEn = @"Class 4 | Amar Bangla Boi | পাঠ ১১ | আমার বাংলা বই",
                YouTubeUrl = "https://www.youtube.com/watch?v=wXtI43EI1C8",
                DurationSeconds = 194,
                TranscriptBn = @"চতুর্থ শ্রেণির শিক্ষার্থীদের উপযোগী বাংলা পাঠ্যসূচির সম্পূর্ণ অডিও সংস্করণ। পাঠ্যক্রমের প্রতিটি শব্দ স্পষ্ট ও সহজবোধ্যভাবে উপস্থাপিত।",
                TranscriptEn = @"Comprehensive audio textbook lesson for Class 4 primary curriculum, mastered specifically for inclusive classrooms and blind learners."
            },
            new()
            {
                Id = 28,
                GradeLevel = "Class 4",
                TitleBn = @"চতুর্থ শ্রেনি | পাঠ ১০ | আমার বাংলা বই",
                TitleEn = @"Class 4 | Amar Bangla Boi | পাঠ ১০ | আমার বাংলা বই",
                YouTubeUrl = "https://www.youtube.com/watch?v=KuJ5boIYLtM",
                DurationSeconds = 482,
                TranscriptBn = @"চতুর্থ শ্রেণির শিক্ষার্থীদের উপযোগী বাংলা পাঠ্যসূচির সম্পূর্ণ অডিও সংস্করণ। পাঠ্যক্রমের প্রতিটি শব্দ স্পষ্ট ও সহজবোধ্যভাবে উপস্থাপিত।",
                TranscriptEn = @"Comprehensive audio textbook lesson for Class 4 primary curriculum, mastered specifically for inclusive classrooms and blind learners."
            },
            new()
            {
                Id = 29,
                GradeLevel = "Class 4",
                TitleBn = @"চতুর্থ শ্রেনি | পাঠ ৯ | আমার বাংলা বই",
                TitleEn = @"Class 4 | Amar Bangla Boi | পাঠ ৯ | আমার বাংলা বই",
                YouTubeUrl = "https://www.youtube.com/watch?v=GQXx7oo-mOg",
                DurationSeconds = 316,
                TranscriptBn = @"চতুর্থ শ্রেণির শিক্ষার্থীদের উপযোগী বাংলা পাঠ্যসূচির সম্পূর্ণ অডিও সংস্করণ। পাঠ্যক্রমের প্রতিটি শব্দ স্পষ্ট ও সহজবোধ্যভাবে উপস্থাপিত।",
                TranscriptEn = @"Comprehensive audio textbook lesson for Class 4 primary curriculum, mastered specifically for inclusive classrooms and blind learners."
            },
            new()
            {
                Id = 30,
                GradeLevel = "Class 4",
                TitleBn = @"চতুর্থ শ্রেনি | পাঠ ৭ | আমার বাংলা বই",
                TitleEn = @"Class 4 | Amar Bangla Boi | পাঠ ৭ | আমার বাংলা বই",
                YouTubeUrl = "https://www.youtube.com/watch?v=yaZRAB-0eco",
                DurationSeconds = 97,
                TranscriptBn = @"চতুর্থ শ্রেণির শিক্ষার্থীদের উপযোগী বাংলা পাঠ্যসূচির সম্পূর্ণ অডিও সংস্করণ। পাঠ্যক্রমের প্রতিটি শব্দ স্পষ্ট ও সহজবোধ্যভাবে উপস্থাপিত।",
                TranscriptEn = @"Comprehensive audio textbook lesson for Class 4 primary curriculum, mastered specifically for inclusive classrooms and blind learners."
            },
            new()
            {
                Id = 31,
                GradeLevel = "Class 4",
                TitleBn = @"চতুর্থ শ্রেনি | পাঠ ৬ | আমার বাংলা বই",
                TitleEn = @"Class 4 | Amar Bangla Boi | পাঠ ৬ | আমার বাংলা বই",
                YouTubeUrl = "https://www.youtube.com/watch?v=UERysDp0fuk",
                DurationSeconds = 124,
                TranscriptBn = @"চতুর্থ শ্রেণির শিক্ষার্থীদের উপযোগী বাংলা পাঠ্যসূচির সম্পূর্ণ অডিও সংস্করণ। পাঠ্যক্রমের প্রতিটি শব্দ স্পষ্ট ও সহজবোধ্যভাবে উপস্থাপিত।",
                TranscriptEn = @"Comprehensive audio textbook lesson for Class 4 primary curriculum, mastered specifically for inclusive classrooms and blind learners."
            },
            new()
            {
                Id = 32,
                GradeLevel = "Class 4",
                TitleBn = @"চতুর্থ শ্রেনি | পাঠ ৫ | আমার বাংলা বই",
                TitleEn = @"Class 4 | Amar Bangla Boi | পাঠ ৫ | আমার বাংলা বই",
                YouTubeUrl = "https://www.youtube.com/watch?v=fkzELOS145c",
                DurationSeconds = 150,
                TranscriptBn = @"চতুর্থ শ্রেণির শিক্ষার্থীদের উপযোগী বাংলা পাঠ্যসূচির সম্পূর্ণ অডিও সংস্করণ। পাঠ্যক্রমের প্রতিটি শব্দ স্পষ্ট ও সহজবোধ্যভাবে উপস্থাপিত।",
                TranscriptEn = @"Comprehensive audio textbook lesson for Class 4 primary curriculum, mastered specifically for inclusive classrooms and blind learners."
            },
            new()
            {
                Id = 33,
                GradeLevel = "Class 4",
                TitleBn = @"চতুর্থ শ্রেনি | পাঠ ২ | আমার বাংলা বই",
                TitleEn = @"Class 4 | Amar Bangla Boi | পাঠ ২ | আমার বাংলা বই",
                YouTubeUrl = "https://www.youtube.com/watch?v=9zjfo-B4iNo",
                DurationSeconds = 141,
                TranscriptBn = @"চতুর্থ শ্রেণির শিক্ষার্থীদের উপযোগী বাংলা পাঠ্যসূচির সম্পূর্ণ অডিও সংস্করণ। পাঠ্যক্রমের প্রতিটি শব্দ স্পষ্ট ও সহজবোধ্যভাবে উপস্থাপিত।",
                TranscriptEn = @"Comprehensive audio textbook lesson for Class 4 primary curriculum, mastered specifically for inclusive classrooms and blind learners."
            },
            new()
            {
                Id = 34,
                GradeLevel = "Class 4",
                TitleBn = @"চতুর্থ শ্রেণি | পাঠ ১ । আমার বাংলা বই |",
                TitleEn = @"Class 4 | Amar Bangla Boi | পাঠ ১ । আমার বাংলা বই |",
                YouTubeUrl = "https://www.youtube.com/watch?v=WwL3M0i6nRY",
                DurationSeconds = 88,
                TranscriptBn = @"চতুর্থ শ্রেণির শিক্ষার্থীদের উপযোগী বাংলা পাঠ্যসূচির সম্পূর্ণ অডিও সংস্করণ। পাঠ্যক্রমের প্রতিটি শব্দ স্পষ্ট ও সহজবোধ্যভাবে উপস্থাপিত।",
                TranscriptEn = @"Comprehensive audio textbook lesson for Class 4 primary curriculum, mastered specifically for inclusive classrooms and blind learners."
            },
            new()
            {
                Id = 35,
                GradeLevel = "Class 4",
                TitleBn = @"আমার বাংলা বই | চতুর্থ শ্রেণি | পাঠ ৫",
                TitleEn = @"Class 4 | Amar Bangla Boi | আমার বাংলা বই | পাঠ ৫",
                YouTubeUrl = "https://www.youtube.com/watch?v=Nvy9KNJ0xTQ",
                DurationSeconds = 205,
                TranscriptBn = @"চতুর্থ শ্রেণির শিক্ষার্থীদের উপযোগী বাংলা পাঠ্যসূচির সম্পূর্ণ অডিও সংস্করণ। পাঠ্যক্রমের প্রতিটি শব্দ স্পষ্ট ও সহজবোধ্যভাবে উপস্থাপিত।",
                TranscriptEn = @"Comprehensive audio textbook lesson for Class 4 primary curriculum, mastered specifically for inclusive classrooms and blind learners."
            },
            new()
            {
                Id = 36,
                GradeLevel = "Class 4",
                TitleBn = @"৪র্থ শ্রেণি, আমার বাংলা বই | বাওয়ালিদের গল্প",
                TitleEn = @"Class 4 | Amar Bangla Boi | বাওয়ালিদের গল্প",
                YouTubeUrl = "https://www.youtube.com/watch?v=cFBFGQEilI8",
                DurationSeconds = 101,
                TranscriptBn = @"চতুর্থ শ্রেণির শিক্ষার্থীদের উপযোগী বাংলা পাঠ্যসূচির সম্পূর্ণ অডিও সংস্করণ। পাঠ্যক্রমের প্রতিটি শব্দ স্পষ্ট ও সহজবোধ্যভাবে উপস্থাপিত।",
                TranscriptEn = @"Comprehensive audio textbook lesson for Class 4 primary curriculum, mastered specifically for inclusive classrooms and blind learners."
            },
            new()
            {
                Id = 37,
                GradeLevel = "Class 4",
                TitleBn = @"Class 4| English For Today | Unit 34",
                TitleEn = @"Class 4 | English For Today | Unit 34: Story - S.M. Sultan",
                YouTubeUrl = "https://www.youtube.com/watch?v=d675GtUMCOw",
                DurationSeconds = 195,
                TranscriptBn = @"চতুর্থ শ্রেণির ইংরেজি পাঠ্যপুস্তক: বাংলাদেশের বিখ্যাত চিত্রশিল্পী এস এম সুলতানের জীবন ও কর্মের অনুপ্রেরণামূলক পাঠ।",
                TranscriptEn = @"Class 4 English lesson on the life, artistic vision, and humanitarian spirit of celebrated master painter S.M. Sultan."
            }
        };

        public SupabaseService(IConfiguration config, ILogger<SupabaseService> logger)
        {
            _config = config;
            _logger = logger;
        }

        public async Task<List<Audiobook>> GetAudiobooksAsync()
        {
            await Task.Delay(10);
            return _audiobooks.ToList();
        }

        public async Task<bool> SubmitFormAsync(FormSubmission submission)
        {
            _logger.LogInformation("Saved new form submission from: {Email}", submission.Email);
            await Task.Delay(10);
            lock (_formSubmissions)
            {
                submission.Id = _nextSubmissionId++;
                submission.CreatedAt = DateTime.UtcNow;
                if (string.IsNullOrWhiteSpace(submission.Status))
                {
                    submission.Status = "New";
                }
                _formSubmissions.Insert(0, submission);
            }
            return true;
        }

        public async Task<List<FormSubmission>> GetSubmissionsAsync()
        {
            await Task.Delay(10);
            lock (_formSubmissions)
            {
                return _formSubmissions.OrderByDescending(s => s.CreatedAt).ToList();
            }
        }

        public async Task<bool> UpdateSubmissionStatusAsync(int id, string status)
        {
            await Task.Delay(10);
            lock (_formSubmissions)
            {
                var item = _formSubmissions.FirstOrDefault(s => s.Id == id);
                if (item != null)
                {
                    item.Status = status;
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> DeleteSubmissionAsync(int id)
        {
            await Task.Delay(10);
            lock (_formSubmissions)
            {
                var item = _formSubmissions.FirstOrDefault(s => s.Id == id);
                if (item != null)
                {
                    _formSubmissions.Remove(item);
                    return true;
                }
            }
            return false;
        }

        public async Task<Profile?> GetProfileAsync(Guid userId)
        {
            await Task.Delay(10);
            return TeamProfiles.FirstOrDefault(p => p.Id == userId) ?? TeamProfiles[0];
        }

        public async Task<bool> UpdateProfileAsync(Profile profile)
        {
            await Task.Delay(10);
            return true;
        }

        public async Task<List<Profile>> GetAllUsersAsync()
        {
            await Task.Delay(10);
            return TeamProfiles;
        }

        public async Task<bool> UpdateUserRoleAsync(Guid userId, string newRole)
        {
            await Task.Delay(10);
            var user = TeamProfiles.FirstOrDefault(p => p.Id == userId);
            if (user != null) user.Role = newRole;
            return true;
        }
    }
}
