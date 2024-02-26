using Microsoft.EntityFrameworkCore;
using StoriesAPI.Infrastruture.Models;
using StoriesAPI.Service.DTO;

namespace StoriesAPI.Service.Service
{
    public class VoteService : IVoteService
    {
        public readonly StoryContext _context;
        private int _trueVoteCount;

        public VoteService(StoryContext storycontext)
        {
            _context = storycontext;
        }

        public async Task<bool> VoteStory(int userId, int storyId, bool vote)
        {
            var existingVote = await _context.Votes.FirstOrDefaultAsync(v => v.UserId == userId && v.StoryId == storyId);

            bool previousVote; 

            if (existingVote != null)
            {
                previousVote = existingVote.Voted; 
                existingVote.Voted = vote; 
            }
            else
            {
                var newVote = new Vote
                {
                    UserId = userId,
                    StoryId = storyId,
                    Voted = vote
                };
                _context.Votes.Add(newVote);

                previousVote = vote; 
            }

            await _context.SaveChangesAsync();

            if (previousVote && !vote) 
            {
                _trueVoteCount--;
            }
            else if (!previousVote && vote) 
            {
                _trueVoteCount++;
            }

            return true;
        }

        private IQueryable<VoteDTO> VotesToDTOs(IQueryable<Vote> votes)
        {
            return votes.Select(vote => new VoteDTO
            {
                Id = vote.Id,
                Voted = vote.Voted,
                UserId = vote.UserId,
                StoryId = vote.StoryId,

                User = new UserDTO
                {
                    Id = vote.UserId,
                    Name = vote.User.Name,
                },

                Story = new StoryDTO
                {
                    Id = vote.StoryId,
                    Title = vote.Story.Title,
                    Description = vote.Story.Description,
                    Departament = vote.Story.Departament,
                }
            });
        }


        public async Task<VoteDTO> DeleteVote(int id)
        {
            var vote = await _context.Stories.FindAsync(id);

            if (vote == null)
            {
                return null;
            }

            _context.Stories.Remove(vote);

            await _context.SaveChangesAsync();

            var deletedVoteDTO = new VoteDTO
            {
                Id = vote.Id,
            };

            return deletedVoteDTO;
        }

        public async Task<UserDTO> AddUser(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            var user = new User
            {
                Name = name,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name
            };
        }

        public async Task<UserDTO> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return null;
            }

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            var deletedUserDTO = new UserDTO
            {
                Id = user.Id,
            };

            return deletedUserDTO;
        }

        public async Task<UserDTO> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return null;
            }

            var userDTO = new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
            };

            return userDTO;
        }
        public async Task<List<UserDTO>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();

            var userDTOs = users.Select(user => new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
            }).ToList();

            return userDTOs;
        }
        public async Task<bool> CheckVote(int userId, int storyId)
        {
            var existingVote = await _context.Votes.FirstOrDefaultAsync(v => v.UserId == userId && v.StoryId == storyId);
            return existingVote != null;
        }
    }

}




