using System;

namespace Models
{
    public class Booking
    {
        public Guid UserId { get; set; }
        public Guid TourId { get; set; }
        public int Seats { get; set; } =  1;
        public User User { get; set; } = new User();
        public Tour Tour { get; set; } = new Tour();
        public int PaymentId { get; set; }
        public Boolean Cancelled { get; set; } = false;
    }
}