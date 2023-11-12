using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Auth
{
    public class SignupUser
    {
        [MaxLength(20,ErrorMessage ="Name cannot be lengthy")]
        public string DisplayName { get; set; } = "";
        public string Email { get; set; } = "";
        [MinLength(8,ErrorMessage ="password should atleast be of 8 characters")]
        public string Password { get; set; } = "";
        public Role Role { get; set; } = Role.Customer;
    }
}