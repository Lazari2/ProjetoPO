using Microsoft.EntityFrameworkCore;
using StoriesAPI.Infrastruture.Models;
using StoriesAPI.Service.DTO;

namespace StoriesAPI.Service.Service
{
    public class StoryService : IStoryService
    {
        public readonly StoryContext _context;

        public StoryService(StoryContext storycontext)
        {
            _context = storycontext;
        }
        
        public async Task<StoryDTO> AddStory(StoryDTO newStory)
        {
            var story = new Story
            {
                Title = newStory.Title,
                Description = newStory.Description,
                Departament = newStory.Departament,
            };
            _context.Stories.Add(story);

            await _context.SaveChangesAsync();

            var addedStoryDTO = new StoryDTO
            {
                Id = story.Id,
                Title = story.Title,
                Description = story.Description,
                Departament = story.Departament,

            };

            return addedStoryDTO;
        }

        public async Task<List<StoryDTO>> GetStories()
        {
            var stories = await _context.Stories.ToListAsync();
            
            var storyDTOs = stories.Select(story => new StoryDTO
            {
                Id=story.Id,
                Title = story.Title,
                Description = story.Description,
                Departament= story.Departament,

            }).ToList();

            return storyDTOs;
        }

        public async Task<StoryDTO> UpdateStory(StoryDTO newStory)
        {
            var existingStory = await _context.Stories.FindAsync(newStory.Id);
            if (existingStory == null)
            {
                
                return null; 
            }

            existingStory.Title = newStory.Title;
            existingStory.Description = newStory.Description;
            existingStory.Departament = newStory.Departament;

            await _context.SaveChangesAsync();

            return newStory;
        }

        public async Task<StoryDTO> DeleteStory(int id)
        {
            var story = await _context.Stories.FindAsync(id);

            if (story == null)
            {
                return null; 
            }

            _context.Stories.Remove(story);

            await _context.SaveChangesAsync();

            var deletedStoryDTO = new StoryDTO
            {
                Id = story.Id,
                Title = story.Title,
                Description = story.Description,
                Departament = story.Departament
            };

            return deletedStoryDTO;
        }

        public async Task<StoryDTO> GetStory(int id)
        {
            var story = await _context.Stories.FindAsync(id);

            if (story == null)
            {
                return null;
            }

            var storyDTO = new StoryDTO
            {
                Id = story.Id,
                Title = story.Title,
                Description = story.Description,
                Departament = story.Departament
            };

            return storyDTO;
        }

        public async Task<int> GetVoteCount(int storyId)
        {
            var story = await _context.Stories.FindAsync(storyId);

            if (story == null)
            {
                return 0; 
            }

            var voteCount = _context.Votes.Count(v => v.StoryId == storyId);

            return voteCount;
        }
    
    }
}
