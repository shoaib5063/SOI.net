using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using StoriesOfInclusion.Services;

namespace StoriesOfInclusion.Components.Pages
{
    public partial class Register : ComponentBase
    {
        public class RegisterInputModel
        {
            [Required(ErrorMessage = "Full Name is required.")]
            [MinLength(2, ErrorMessage = "Name must be at least 2 characters.")]
            public string FullName { get; set; } = "";

            [Required(ErrorMessage = "Email address is required.")]
            [EmailAddress(ErrorMessage = "Please enter a valid email format.")]
            public string Email { get; set; } = "";

            public string Specialty { get; set; } = "Voice Artist & Narrator";

            [Required(ErrorMessage = "Password is required.")]
            [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
            public string Password { get; set; } = "";

            [Required(ErrorMessage = "Please confirm your password.")]
            public string ConfirmPassword { get; set; } = "";

            [Range(typeof(bool), "true", "true", ErrorMessage = "You must agree to the Terms and Privacy Policy.")]
            public bool AgreeToTerms { get; set; } = false;
        }

        private RegisterInputModel RegisterModel { get; set; } = new();
        private bool ShowPassword { get; set; } = false;
        private bool IsLoading { get; set; } = false;
        private string? ErrorMessage { get; set; }
        private string? SuccessMessage { get; set; }

        private void TogglePasswordVisibility()
        {
            ShowPassword = !ShowPassword;
        }

        private async Task HandleRegister()
        {
            if (RegisterModel.Password != RegisterModel.ConfirmPassword)
            {
                ErrorMessage = "Passwords do not match. Please re-enter both passwords.";
                return;
            }

            if (!RegisterModel.AgreeToTerms)
            {
                ErrorMessage = "Please agree to the Terms of Service and Privacy Policy to proceed.";
                return;
            }

            IsLoading = true;
            ErrorMessage = null;
            SuccessMessage = null;

            await Task.Delay(500);

            // Register user as Member (User role)
            AppState.Login(RegisterModel.Email.Trim(), RegisterModel.FullName.Trim(), AppRole.User);
            SuccessMessage = $"Welcome to Stories of Inclusion, {RegisterModel.FullName}! Your member account has been created. Redirecting to your dashboard...";
            StateHasChanged();

            await Task.Delay(700);
            IsLoading = false;
            Navigation.NavigateTo("dashboard/user");
        }
    }
}
