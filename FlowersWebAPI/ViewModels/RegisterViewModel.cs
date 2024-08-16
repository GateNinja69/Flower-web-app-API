using System.ComponentModel.DataAnnotations;

namespace FlowersWebAPI.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string? UserName {  get; set; }
        [Required]
        [EmailAddress] 
        public string? Email { get; set;}

        [Required]
        public string? password {  get; set; }
    }
}
