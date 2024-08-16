using FlowrAAppAPI.Data;
using FlowrAAppAPI.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace FlowrAAppAPI.Repository
{
    public class FlowerRepository : IFlowerRepository
    {
        private readonly AppDBContext context;

        public FlowerRepository(AppDBContext context)
        {
            this.context = context;
        }
        public async Task<Flowers> AddNewFlower(Flowers flowers)
        {
            //Flowers flowers1 = new Flowers()
            //{
            //    FlowerName = flowers.FlowerName,
            //    FlowerDescription = flowers.FlowerDescription,
            //    FlowerRate = flowers.FlowerRate,
            //    PicPath = flowers.PicPath
            //};
            var NewFlower = await context.FlowersDetails.AddAsync(flowers);
            await context.SaveChangesAsync();
            return flowers;
        }

        public async Task<Flowers> DeleteFlower(int id)
        {
            var result = await  context.FlowersDetails.FindAsync(id);
            if (result == null) {
                return null;
            }
            context.FlowersDetails.Remove(result);
            await context.SaveChangesAsync();
            return result;
        }

        public async Task<Flowers> EditFlower( Flowers flowers)
        {
            var FlowerToEdit = await context.FlowersDetails.FirstOrDefaultAsync(i=> i.FlowerID == flowers.FlowerID);
            if (FlowerToEdit !=null )
            {
               // FlowerToEdit.FlowerID = flowers.FlowerID;
                FlowerToEdit.FlowerName = flowers.FlowerName;
                FlowerToEdit.FlowerDescription = flowers.FlowerDescription;
                FlowerToEdit.FlowerRate = flowers.FlowerRate;
                FlowerToEdit.PicPath = flowers.PicPath;
                await context.SaveChangesAsync();
                return flowers;
            }
            
            return null;

        }

        public async Task<List<Flowers>> GetAllFlower()
        {
            var result = await context.FlowersDetails.ToListAsync();
            return result;
        }

        public async Task<Flowers> GetFlowerById(int id)
        {
            var result = await context.FlowersDetails.FindAsync(id);
            if(result == null)
            {
                return null;
            }
            return result;
        }




    }
}
