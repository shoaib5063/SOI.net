using Microsoft.AspNetCore.Mvc;

namespace StoriesOfInclusion.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html"), "text/html");
        
        [Route("about")]
        public IActionResult About() => PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "about.html"), "text/html");

        [Route("activities")]
        public IActionResult Activities() => PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "activities.html"), "text/html");

        [Route("audiobooks")]
        public IActionResult Audiobooks() => PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "audiobooks.html"), "text/html");

        [Route("recognition")]
        public IActionResult Recognition() => PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "recognition.html"), "text/html");

        [Route("team")]
        public IActionResult Team() => PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "team.html"), "text/html");

        [Route("contact")]
        public IActionResult Contact() => PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "contact.html"), "text/html");

        // Role-Based Dashboards
        [Route("dashboard/user")]
        public IActionResult UserDashboard() => PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "dashboard-user.html"), "text/html");

        [Route("dashboard/admin")]
        public IActionResult AdminDashboard() => PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "dashboard-admin.html"), "text/html");

        [Route("dashboard/superadmin")]
        public IActionResult SuperAdminDashboard() => PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "dashboard-superadmin.html"), "text/html");
    }
}
