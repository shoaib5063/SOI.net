using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;

namespace StoriesOfInclusion.Components.Pages
{
    public partial class ForgotPassword : ComponentBase
    {
        [Required(ErrorMessage = "Please enter your registered email address.")]
        [EmailAddress(ErrorMessage = "Please provide a valid email address.")]
        public string Email { get; set; } = "";

        private bool IsLoading { get; set; } = false;
        private bool IsSubmitted { get; set; } = false;
        private string? ErrorMessage { get; set; }
        private string ResetCode { get; set; } = "";

        private async Task HandleSubmit()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                ErrorMessage = "Please enter your email address.";
                return;
            }

            IsLoading = true;
            ErrorMessage = null;

            await Task.Delay(500);

            ResetCode = new Random().Next(100000, 999999).ToString();
            IsLoading = false;
            IsSubmitted = true;
        }

        private void ResetForm()
        {
            Email = "";
            IsSubmitted = false;
            ErrorMessage = null;
        }
    }
}
