using System.ComponentModel.DataAnnotations;

namespace FlowersWebAPI.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string UserName {  get; set; }
        [Required]
        public string Password { get; set; }
    }
}
