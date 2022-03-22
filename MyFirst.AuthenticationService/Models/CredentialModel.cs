using System.ComponentModel.DataAnnotations;

namespace MyFirst.AuthenticationService.Models
{
    public class CredentialModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password  { get; set; }
    }
}
