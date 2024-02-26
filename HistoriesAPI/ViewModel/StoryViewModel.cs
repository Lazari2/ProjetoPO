using System.ComponentModel.DataAnnotations;
using StoriesAPI.Infrastruture.Models;

namespace StoriesAPI.ViewModel
{
    public class StoryViewModel
    {
            public int Id { get; set; }
           
            public string Title { get; set; }
            
            public string Description { get; set; }
            
            public string Departament { get; set; }                        
    }
}


