using System;

namespace Models
{
    public class Tour
    {
        public Guid Id { get; set; } 
        public Guid PlaceId { get; set; }
        public Place Place { get; set; } = new Place();
        public int Days { get; set; }
        public double Price { get; set; }
        public int TotalSeats { get; set; }
        public IList<Booking> Bookings { get; set; } = new List<Booking>();
        public IList<Review> Reviews { get; set; } = new List<Review>();
    }
}