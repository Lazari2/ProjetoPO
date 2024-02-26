using StoriesAPI.Service.DTO;

namespace StoriesAPI.Service.Service
{
    public interface IStoryService
    {
        Task<StoryDTO> AddStory(StoryDTO newStory);
        Task<List<StoryDTO>> GetStories();
        Task<StoryDTO> UpdateStory(StoryDTO newStory);
        Task<StoryDTO> DeleteStory(int id);
        Task<StoryDTO> GetStory(int id);
        Task<int> GetVoteCount(int storyId);
    }
}
