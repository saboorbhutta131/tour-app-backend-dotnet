using System;
using System.ComponentModel.DataAnnotations;

namespace Models.ControllerActions._Place
{
    public class CreatePlace
    {
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Image { get; set; } = "";
    }
    
    public class ViewPlace: CreatePlace{
        public Guid Id { get; set; }
    }

    public class ViewPlaceWithTours : ViewPlace{
        public IList<Tour> Tours { get; set; } =  new List<Tour>();
    } 

    public class EditPlace{
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
    }

}