using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Practice_1._1.Models
{
    public class RegisterVM
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress,ErrorMessage ="This is not a valid email adress")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]                //set the data type of this property to password
        [StringLength(20, ErrorMessage ="The Password must be at least {2} characters long", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage ="Password confirmation is required")]
        [Compare("Password",ErrorMessage ="Password does not match")]  //the compare attribute compares the property below the annotation and the property inside the parenthesis after the comma we add an error message 
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}