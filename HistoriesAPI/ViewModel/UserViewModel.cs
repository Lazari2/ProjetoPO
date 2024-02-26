using StoriesAPI.Infrastruture.Models;

namespace StoriesAPI.ViewModel
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Vote> Votes { get; set; }
    }
}

