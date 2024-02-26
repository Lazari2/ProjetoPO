using System.Net;
using Microsoft.AspNetCore.Mvc;
using StoriesAPI.Service.Service;
using StoriesAPI.ViewModel;

namespace StoriesAPI.Application.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        private readonly IVoteService _voteService;

        public VoteController(IVoteService voteService)
        {
            _voteService = voteService;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> VoteStory(int userId, int storyId, bool vote)
       {
           bool result = await _voteService.VoteStory(userId, storyId, vote);

           if (result)
            {
                return Ok();
           }
            else
           {
                return BadRequest("Error.");
           }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteVote(int id)
        {
            var deletedVote = await _voteService.DeleteVote(id);

            if (deletedVote == null)
            {
                return NotFound("Vote not founded.");
            }

            return Ok(deletedVote);
        }


        [HttpPost("AddUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<UserViewModel>> AddUser(string name)
        {
           var user = await _voteService.AddUser(name);
           
            if (user == null)
            {
                return BadRequest();
            }

            return Ok(user);
       }

        [HttpDelete("DeleteUser{id}" )]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deletedUser = await _voteService.DeleteUser(id);

            if (deletedUser == null)
            {
                return NotFound("User not founded.");
            }

            return Ok(deletedUser);
        }

        [HttpGet("GetUser{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _voteService.GetUser(id);

            if (user == null)
            {
                return NotFound("User not Founded.");
            }

            return Ok(user);
        }

        [HttpGet("User")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _voteService.GetUsers();

            if (users == null)
            {
                return NotFound("User not Founded.");
            }

            return Ok(users);
        }

        [HttpGet("CheckVote")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<bool> CheckVote(int userId, int storyId)
        {
            return await _voteService.CheckVote(userId, storyId);
        }

    }
}
