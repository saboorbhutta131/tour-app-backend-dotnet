using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class User
    {
        public Guid Id { get; set; } 
        [Column(TypeName="VARCHAR(100)")]
        public string DisplayName { get; set; } = "";    
        [Column(TypeName="VARCHAR(100)")]
        public string Email { get; set; } = "";
        [MaxLength(8,ErrorMessage ="Password cannot have more than 8 characters")]
        public string Password { get; set; } = "";
        public Role Role { get; set; }
        public IList<Booking> Bookings { get; set; } = new List<Booking>();
        public IList<Review> Reviews { get; set; } = new List<Review>();
    }
}