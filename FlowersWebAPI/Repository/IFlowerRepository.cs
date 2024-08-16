using FlowrAAppAPI.Model;

namespace FlowrAAppAPI.Repository
{
    public interface IFlowerRepository
    {
        Task<List<Flowers>> GetAllFlower();
        Task<Flowers> GetFlowerById(int id);
        Task<Flowers> EditFlower( Flowers flowers);
        Task<Flowers> AddNewFlower(Flowers flowers);
        Task<Flowers> DeleteFlower(int id);
    }
}
