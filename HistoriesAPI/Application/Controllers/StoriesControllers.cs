using System.Net;
using Microsoft.AspNetCore.Mvc;
using StoriesAPI.Service.DTO;
using StoriesAPI.Service.Service;
using StoriesAPI.ViewModel;

namespace StoriesAPI.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoriesController : ControllerBase
    {
        private readonly IStoryService _storyService;

        public StoriesController(IStoryService storyService)
        {
            _storyService = storyService;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddStory(StoryViewModel newStory)
        {
            var newStoryDTO = new StoryDTO
            {
                Title = newStory.Title,
                Description = newStory.Description,
                Departament = newStory.Departament,
            };

            var addedStory = await _storyService.AddStory(newStoryDTO);

            return Ok(addedStory);
        }

        [HttpGet]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(400)]

        public async Task<IActionResult> GetStories()
        {
            var stories = await _storyService.GetStories();

            return Ok(stories);
        }

        [HttpPut("{id}")]
        [ProducesResponseType( (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateStory(int id, StoryViewModel updatedStory)
        {
           
            var updatedStoryDTO = new StoryDTO
            {
                Id = updatedStory.Id,
                Title = updatedStory.Title,
                Description = updatedStory.Description,
                Departament = updatedStory.Departament
            };

            
            var result = await _storyService.UpdateStory(updatedStoryDTO);

           
            if (result == null)
            {
                return NotFound("Story not Founded.");
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteStory(int id)
        {
            var deletedStory = await _storyService.DeleteStory(id);

            if (deletedStory == null)
            {
                return NotFound("Story not founded.");
            }

            return Ok(deletedStory);
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetStory(int id)
        {
            var story = await _storyService.GetStory(id);

            if (story == null)
            {
                return NotFound("Story not Founded.");
            }

            return Ok(story);
        }

        [HttpGet("count/{storyId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetVoteCount(int storyId)
        {
            var voteCount = await _storyService.GetVoteCount(storyId);
            if (voteCount >= 0)
            {
                return Ok(voteCount);
            }
            else
            {
                return BadRequest("Unable to retrieve vote count for the specified story.");
            }
        }


    }
}
