using System;
using System.ComponentModel.DataAnnotations;

namespace Models.ControllerActions._Tour
{
    public class CreateTour
    {
        public Guid PlaceId { get; set; }
        public int Days { get; set; }
        public double Price { get; set; }
        public int TotalSeats { get; set; }
    }

    public class EditTour: CreateTour
    {
        public Guid Id { get; set; }
    }
}