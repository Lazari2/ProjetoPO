﻿namespace StoriesAPI.Infrastruture.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Vote> Votes { get; set; }

    }
}
