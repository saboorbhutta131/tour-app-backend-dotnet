using System;
using System.ComponentModel.DataAnnotations;

namespace Models.ControllerActions._Review
{
    public class CreateReview{
        public int Rating { get; set; }
        public string Comment { get; set; } = "";
        public Guid TourId { get; set; }
        public Guid UserId { get; set; }

    }

}