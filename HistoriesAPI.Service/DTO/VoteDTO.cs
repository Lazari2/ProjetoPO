using StoriesAPI.Infrastruture.Models;

namespace StoriesAPI.Service.DTO
{
    public class VoteDTO
    {
        public int Id { get; set; }
        public bool Voted { get; set; }

        public int UserId { get; set; }
        public UserDTO User { get; set; }

        public int StoryId { get; set; }
        public StoryDTO Story { get; set; }
    }
}
