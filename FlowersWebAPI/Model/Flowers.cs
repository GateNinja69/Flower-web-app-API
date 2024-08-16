using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FlowrAAppAPI.Model
{
    public class Flowers
    {
        //PrimaryKey
        [Key]
        [Required]
        public int FlowerID { get; set; }
        [Required]
        public string? FlowerName {  get; set; }
        [Required]
        public string? FlowerDescription { get; set;}
        [Required]
        public double FlowerRate { get; set;}
        public string? PicPath {  get; set;}
    }
}
