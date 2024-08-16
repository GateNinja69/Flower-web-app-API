using System.ComponentModel.DataAnnotations;

namespace FlowrAAppAPI.ViewModel
{
    public class FlowerViewModel 
    {
        [Required]
        public string? FlowerName { get; set; }
        [Required]
        public string? FlowerDescription { get; set; }
        [Required]
        public double FlowerRate { get; set; }
        public IFormFile? PicPath { get; set; }
    }
}
