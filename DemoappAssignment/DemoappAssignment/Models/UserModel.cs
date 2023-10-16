using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemoappAssignment.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage ="Please provide your Birth date in format:yyyy-MM-dd")]
        [DisplayName("Date Of Birth")]
        public string Birthdate { get; set; }
        [Required(ErrorMessage = "Email required")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Wrong email format")]
        public string Email { get; set; }
        [RegularExpression(@"^\d+$", ErrorMessage = "Allow only numbers")]
        [Required(ErrorMessage = "Mobile Number required")]
        [DisplayName("Mobile Number")]
        [MaxLength(10,ErrorMessage ="Mobile number allow only 10 numbers")]
        public string MobileNumber { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }

    }
}