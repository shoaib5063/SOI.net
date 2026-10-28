using StoriesOfInclusion.Models;

namespace StoriesOfInclusion.Services
{
    public interface ISupabaseService
    {
        Task<List<Audiobook>> GetAudiobooksAsync();
        Task<bool> SubmitFormAsync(FormSubmission submission);
        Task<List<FormSubmission>> GetSubmissionsAsync();
        Task<Profile?> GetProfileAsync(Guid userId);
        Task<bool> UpdateProfileAsync(Profile profile);
        Task<List<Profile>> GetAllUsersAsync();
        Task<bool> UpdateUserRoleAsync(Guid userId, string newRole);
    }

    public class SupabaseService : ISupabaseService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<SupabaseService> _logger;

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
            new() { Id = Guid.NewGuid(), FullName = "Ismail Hosen Polash", Email = "ismail.polash@storiesofinclusion.org", Role = "user", BioEn = "Audiobook studio recording, audio mastering, and video production.", IsPortfolioApproved = true },
            new() { Id = Guid.NewGuid(), FullName = "Shoaib Ahmed", Email = "shoaibahmed.5063@gmail.com", Role = "admin", BioEn = "Lead Developer & Accessibility Architect.", IsPortfolioApproved = true }
        };

        public SupabaseService(IConfiguration config, ILogger<SupabaseService> logger)
        {
            _config = config;
            _logger = logger;
        }

        public async Task<List<Audiobook>> GetAudiobooksAsync()
        {
            await Task.Delay(10);
            return new List<Audiobook>();
        }

        public async Task<bool> SubmitFormAsync(FormSubmission submission)
        {
            _logger.LogInformation("Saved new form submission from: {Email}", submission.Email);
            await Task.Delay(10);
            return true;
        }

        public async Task<List<FormSubmission>> GetSubmissionsAsync()
        {
            await Task.Delay(10);
            return new List<FormSubmission>();
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
