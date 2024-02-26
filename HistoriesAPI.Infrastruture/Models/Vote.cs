namespace StoriesAPI.Infrastruture.Models
{
   public class Vote
    {
        public int Id { get; set; }
        public bool Voted { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public int StoryId { get; set; }
        public Story Story { get; set; }
    }
}
