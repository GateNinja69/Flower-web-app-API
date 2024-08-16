using FlowrAAppAPI.Data;
using FlowrAAppAPI.Model;
using FlowrAAppAPI.Repository;
using FlowrAAppAPI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;

namespace FlowrAAppAPI.Controllers
{
    
    [Route("api/flower")]
    [ApiController]
    public class FlowerController : ControllerBase
    {
        private readonly IFlowerRepository repository;

        private readonly IWebHostEnvironment HostEnvironment;

        public FlowerController(IFlowerRepository repository, IWebHostEnvironment HostEnvironment)
        {
            this.repository = repository;
            this.HostEnvironment = HostEnvironment;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllFlowers()
        {
            var result = await repository.GetAllFlower();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize]

        public async Task<IActionResult> GetById(int id)
        {
            var result = await repository.GetFlowerById(id);
            if(result == null)
            {
                return NotFound($"The Flower with {id} not found");
            }
            return Ok(result);
        }
        //[HttpGet("{id}")]
        [HttpPut("{id}")]
        [Authorize]

        public async Task<IActionResult> UpdateFlower(int id, EditFlowerViewModel flower )
        {
            

            var FlowerToUpdate = await repository.GetFlowerById(id);
            if(FlowerToUpdate == null)
            {
                return NotFound($"The Flower with {id} not found");
            }
            FlowerToUpdate.FlowerName = flower.FlowerName;
            FlowerToUpdate.FlowerRate = flower.FlowerRate;
            FlowerToUpdate.FlowerDescription = flower.FlowerDescription;

            if (flower.PicPath != null)
            {
                if(FlowerToUpdate.PicPath!= null)
                {
                    var FilePath =  Path.Combine(HostEnvironment.WebRootPath, "images", FlowerToUpdate.PicPath);
                    System.IO.File.Delete(FilePath);
                }
                string uniqueFileName = null;
                if (flower.PicPath != null)
                {
                    string uploadsFolder = Path.Combine(HostEnvironment.WebRootPath, "images");   //here we select the folder path where we will store picture
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + flower.PicPath;   //it is used to make every picture name different if there is same name pictures
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);      // here we will combine the file and folder 
                    flower.PicPath.CopyTo(new FileStream(filePath, FileMode.Create));     // 

                }
                FlowerToUpdate.PicPath = uniqueFileName;
            }
            var result = await repository.EditFlower( FlowerToUpdate);
            return Ok(result);
            
        }

        [HttpDelete("{id}")]
        [Authorize]

        public async Task<IActionResult> DeleteFlowers(int id)
        {
            var result = await repository.GetFlowerById(id);
            if(result == null)
            {
                return NotFound($"The Flower with {id} not found");
            }
            await repository.DeleteFlower(result.FlowerID);
            return Ok("flower has been successfully deleted");
        }

        [HttpPost]
        [Authorize]

        public async Task<IActionResult> AddFlowers(FlowerViewModel flower)
        {
            string uniqueFileName = null;
            if (flower.PicPath != null)
            {
                string uploadsFolder = Path.Combine(HostEnvironment.WebRootPath, "images");   //here we select the folder path where we will store picture
                uniqueFileName = Guid.NewGuid().ToString() + "_" + flower.PicPath;   //it is used to make every picture name different if there is same name pictures
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);      // here we will combine the file and folder 
                flower.PicPath.CopyTo(new FileStream(filePath, FileMode.Create));     // 

            }

            Flowers flower1 = new Flowers()
            {
                FlowerName = flower.FlowerName,
                FlowerDescription = flower.FlowerDescription,
                FlowerRate = flower.FlowerRate,
                PicPath = uniqueFileName
            };

            var newflower = await repository.AddNewFlower(flower1 );
            return Ok(newflower);
            
        }
    }
}
