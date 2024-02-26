namespace StoriesAPI.Infrastruture.Models
{
    public class Story
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Departament { get; set; }

        public ICollection<Vote> Votes { get; set; }
    }
}
