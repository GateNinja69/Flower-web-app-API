using System.ComponentModel.DataAnnotations;

namespace FlowrAAppAPI.ViewModel
{
    public class EditFlowerViewModel : FlowerViewModel
    {
        public int id {  get; set; }
       
        public IFormFile? ExistingPicPath { get; set; }
    }
}
