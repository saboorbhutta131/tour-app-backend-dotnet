using System;

namespace Models
{
    public class Review
    {
        public Guid Id { get; set; }    
        public int Rating { get; set; }
        public string Comment { get; set; } = "";
        public Guid TourId { get; set; }
        public Guid UserId { get; set; }
        public Tour Tour { get; set; } = new Tour();
        public User User { get; set; } =  new User();
    }
}