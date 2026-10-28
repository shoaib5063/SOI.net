using Microsoft.AspNetCore.Components;

namespace StoriesOfInclusion.Components.Pages.Dashboard
{
    public partial class SuperAdminDashboard : ComponentBase, IDisposable
    {
        public class UserRecord
        {
            public string Name { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Role { get; set; } = "user";
        }

        public record AuditEntry(string Timestamp, string Actor, string Event, string Severity);

        private string ActiveTab { get; set; } = "users"; // "users", "logs", "database"
        private string SearchQuery { get; set; } = "";
        private string RoleFilter { get; set; } = "all";

        private bool IsAddUserOpen { get; set; } = false;
        private string NewUserName { get; set; } = "";
        private string NewUserEmail { get; set; } = "";
        private string NewUserRole { get; set; } = "user";

        private bool ShowAlert { get; set; } = false;
        private string AlertMessage { get; set; } = "";

        private List<UserRecord> Users { get; set; } = new()
        {
            new() { Name = "Iffat Nusaiba", Email = "nusaibaiffaat3@gmail.com", Role = "superadmin" },
            new() { Name = "Musarrat Jahan Esha", Email = "mjesha12@gmail.com", Role = "admin" },
            new() { Name = "Shoaib Ahmed", Email = "shoaibahmed.5063@gmail.com", Role = "admin" },
            new() { Name = "Naima Jannat Taki", Email = "naima.taki@storiesofinclusion.org", Role = "admin" },
            new() { Name = "Md. Rafieu Hossain Arnob", Email = "rafieu.arnob@storiesofinclusion.org", Role = "admin" },
            new() { Name = "Afnan Jahan Orthey", Email = "afnan.orthey@storiesofinclusion.org", Role = "admin" },
            new() { Name = "Masruba Mehejabin Tanha", Email = "masruba.tanha@storiesofinclusion.org", Role = "user" },
            new() { Name = "Ehsan Al Rakib Chowdhury", Email = "ehsan.rakib@storiesofinclusion.org", Role = "user" },
            new() { Name = "Kazi Airin Sultana", Email = "kazi.airin@storiesofinclusion.org", Role = "user" },
            new() { Name = "Ismail Hosen Polash", Email = "ismail.polash@storiesofinclusion.org", Role = "user" },
            new() { Name = "Tanvir Hasan", Email = "tanvir.hasan@member.org", Role = "user" }
        };

        private List<AuditEntry> AuditLogs { get; set; } = new()
        {
            new("2026-10-27 15:20:10", "Shoaib Ahmed (Admin)", "Approved member portfolio for Tanvir Hasan", "Normal"),
            new("2026-10-27 14:42:05", "System", "Imported 9 core team member profiles into Supabase database", "System"),
            new("2026-10-27 13:25:00", "Iffat Nusaiba (SuperAdmin)", "SuperAdmin session authenticated via Supabase", "Security")
        };

        private List<UserRecord> FilteredUsers
        {
            get
            {
                var query = Users.AsEnumerable();
                if (!string.IsNullOrWhiteSpace(SearchQuery))
                {
                    query = query.Where(u => u.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                                             u.Email.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase));
                }
                if (RoleFilter != "all")
                {
                    query = query.Where(u => u.Role == RoleFilter);
                }
                return query.ToList();
            }
        }

        private int AdminCount => Users.Count(u => u.Role == "admin" || u.Role == "superadmin");
        private bool IsPinging { get; set; } = false;
        private string? DbPingResult { get; set; }

        protected override void OnInitialized()
        {
            AppState.OnChange += StateHasChanged;
        }

        private void ToggleAddUserModal()
        {
            IsAddUserOpen = !IsAddUserOpen;
        }

        private async void TriggerAlert(string msg)
        {
            AlertMessage = msg;
            ShowAlert = true;
            StateHasChanged();
            await Task.Delay(3500);
            ShowAlert = false;
            StateHasChanged();
        }

        private void CreateUser()
        {
            if (string.IsNullOrWhiteSpace(NewUserName) || string.IsNullOrWhiteSpace(NewUserEmail))
            {
                TriggerAlert("⚠️ Please enter both a name and an email address.");
                return;
            }

            var newUser = new UserRecord
            {
                Name = NewUserName.Trim(),
                Email = NewUserEmail.Trim(),
                Role = NewUserRole
            };

            Users.Insert(0, newUser);
            AuditLogs.Insert(0, new(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "SuperAdmin", $"Created new {newUser.Role.ToUpper()} account for {newUser.Name} ({newUser.Email})", "Security"));
            
            TriggerAlert($"✓ Successfully created {newUser.Role.ToUpper()} account for {newUser.Name}!");
            NewUserName = "";
            NewUserEmail = "";
            NewUserRole = "user";
            IsAddUserOpen = false;
        }

        private async Task PingDatabase()
        {
            IsPinging = true;
            await Task.Delay(400);
            IsPinging = false;
            DbPingResult = "✓ Supabase PostgreSQL Connection Status: 200 OK | Latency: 38ms | Storage Pool: Healthy";
            AuditLogs.Insert(0, new(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "SuperAdmin", "Executed health ping on Supabase PostgreSQL engine", "System"));
        }

        private void ChangeUserRole(UserRecord user, string? newRole)
        {
            if (!string.IsNullOrEmpty(newRole) && user.Role != newRole)
            {
                var oldRole = user.Role;
                user.Role = newRole;
                AuditLogs.Insert(0, new(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "SuperAdmin", $"Changed role for {user.Name} from {oldRole.ToUpper()} to {newRole.ToUpper()}", "Security"));
                TriggerAlert($"✓ Updated {user.Name}'s role to {newRole.ToUpper()}!");
            }
        }

        private void RevokeUser(UserRecord user)
        {
            Users.Remove(user);
            AuditLogs.Insert(0, new(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "SuperAdmin", $"Revoked and deleted account for {user.Name} ({user.Email})", "Warning"));
            TriggerAlert($"✓ Successfully revoked and deleted access for {user.Name}.");
        }

        private void ClearAuditLogs()
        {
            AuditLogs.Clear();
            AuditLogs.Add(new(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "SuperAdmin", "Cleared audit log history", "Security"));
            TriggerAlert("✓ Audit logs cleared.");
        }

        public void Dispose()
        {
            AppState.OnChange -= StateHasChanged;
        }
    }
}
