using System;
using System.ComponentModel.DataAnnotations;

namespace Models.ControllerActions._Booking
{
    public class CreateBooking{
        public Guid UserId { get; set; }
        public Guid TourId { get; set; }
        public int Seats { get; set; }
        public int PaymentId { get; set; }
    }

}