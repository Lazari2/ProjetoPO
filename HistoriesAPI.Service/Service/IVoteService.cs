using StoriesAPI.Service.DTO;

namespace StoriesAPI.Service.Service
{
    public interface IVoteService
    {
        Task<bool>VoteStory(int userId, int storyId, bool vote);
        Task<UserDTO>AddUser(string name);
        Task<VoteDTO>DeleteVote(int id);
        Task<UserDTO>DeleteUser(int id);
        Task<UserDTO>GetUser(int id);
        Task<List<UserDTO>> GetUsers();
        Task<bool> CheckVote(int userId, int storyId);
    }
}
