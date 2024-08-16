using FlowrAAppAPI.Model;

namespace FlowersWebAPI.Repository
{
    public interface ITokenRepository
    {
        string CreateToken(User user);
    }
}
