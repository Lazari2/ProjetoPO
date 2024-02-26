using StoriesAPI.Infrastruture.Models;

namespace StoriesAPI.Service.DTO
{
    public class StoryDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Departament { get; set; }

        public ICollection<Vote> Votes { get; set; }
    }
}
