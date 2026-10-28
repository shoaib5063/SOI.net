using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using StoriesOfInclusion.Models;
using StoriesOfInclusion.Services;

namespace StoriesOfInclusion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly ISupabaseService _supabase;

        public ApiController(ISupabaseService supabase)
        {
            _supabase = supabase;
        }

        [HttpGet("audiobooks")]
        public async Task<IActionResult> GetAudiobooks()
        {
            var books = await _supabase.GetAudiobooksAsync();
            return Ok(books);
        }

        [HttpPost("submissions")]
        public async Task<IActionResult> SubmitForm([FromBody] FormSubmission submission)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var success = await _supabase.SubmitFormAsync(submission);
            return Ok(new { success, message = "Submission recorded successfully." });
        }

        [HttpGet("admin/submissions")]
        [Authorize(Policy = "AdminAndAbove")]
        public async Task<IActionResult> GetSubmissions()
        {
            var list = await _supabase.GetSubmissionsAsync();
            return Ok(list);
        }

        [HttpPost("superadmin/change-role")]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> ChangeRole([FromQuery] Guid userId, [FromQuery] string newRole)
        {
            var success = await _supabase.UpdateUserRoleAsync(userId, newRole);
            return Ok(new { success });
        }
    }
}
