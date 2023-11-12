using System;

namespace Models
{
    public class Place
    {
        public Guid Id { get; set; }    
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Image { get; set; } = "";
        public IList<Tour> Tours { get; set; } =  new List<Tour>();
    }
}