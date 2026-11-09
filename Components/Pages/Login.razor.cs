using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using StoriesOfInclusion.Services;

namespace StoriesOfInclusion.Components.Pages
{
    public partial class Login : ComponentBase
    {
        public class LoginInputModel
        {
            [Required(ErrorMessage = "Please enter your registered email address.")]
            [EmailAddress(ErrorMessage = "Please provide a valid email format (e.g. name@domain.com).")]
            public string Email { get; set; } = "";

            [Required(ErrorMessage = "Password is required to sign in.")]
            [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
            public string Password { get; set; } = "";

            public bool RememberMe { get; set; } = true;
        }

        private LoginInputModel LoginModel { get; set; } = new();
        private bool ShowPassword { get; set; } = false;
        private bool IsLoading { get; set; } = false;
        private string? ErrorMessage { get; set; }
        private string? SuccessMessage { get; set; }

        private void TogglePasswordVisibility()
        {
            ShowPassword = !ShowPassword;
        }

        private async Task LoginWithGoogle()
        {
            IsLoading = true;
            ErrorMessage = null;
            SuccessMessage = "Signing in with Google...";
            StateHasChanged();

            await Task.Delay(450);
            AppState.Login("contributor@gmail.com", "Google Contributor", AppRole.User);
            SuccessMessage = "Authenticated! Redirecting to Member Dashboard...";
            StateHasChanged();

            await Task.Delay(400);
            IsLoading = false;
            Navigation.NavigateTo("dashboard/user");
        }

        private async Task LoginWithFacebook()
        {
            IsLoading = true;
            ErrorMessage = null;
            SuccessMessage = "Signing in with Facebook...";
            StateHasChanged();

            await Task.Delay(450);
            AppState.Login("contributor@facebook.com", "Facebook Contributor", AppRole.User);
            SuccessMessage = "Authenticated! Redirecting to Member Dashboard...";
            StateHasChanged();

            await Task.Delay(400);
            IsLoading = false;
            Navigation.NavigateTo("dashboard/user");
        }

        private async Task HandleLogin()
        {
            IsLoading = true;
            ErrorMessage = null;
            SuccessMessage = null;

            // Small tactile delay for realistic async sign-in feel
            await Task.Delay(400);

            var email = LoginModel.Email.Trim().ToLowerInvariant();
            AppRole role;
            string fullName;
            string redirectTarget;

            if (email.Contains("nusaiba") || email == "nusaibaiffaat3@gmail.com")
            {
                role = AppRole.SuperAdmin;
                fullName = "Iffat Nusaiba";
                redirectTarget = "dashboard/superadmin";
            }
            else if (email.Contains("admin") || email == "admin@storiesofinclusion.org" || email == "staff@storiesofinclusion.org")
            {
                role = AppRole.Admin;
                fullName = "Staff Admin";
                redirectTarget = "dashboard/admin";
            }
            else
            {
                role = AppRole.User;
                fullName = email.Contains("masruba") ? "Masruba Mehejabin Tanha" : (email.Split('@')[0]);
                redirectTarget = "dashboard/user";
            }

            AppState.Login(LoginModel.Email.Trim(), fullName, role);
            SuccessMessage = $"Welcome back, {fullName}! Redirecting to your {AppState.GetRoleDisplayName(role)} dashboard...";
            StateHasChanged();

            await Task.Delay(600);
            IsLoading = false;
            Navigation.NavigateTo(redirectTarget);
        }
    }
}
