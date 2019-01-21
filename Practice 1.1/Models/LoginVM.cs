using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Practice_1._1.Models
{
    public class LoginVM
    {
        [Required(ErrorMessage ="Please type a username")]
        public string Username { get; set; }

        [Required(ErrorMessage ="Please type a password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }

    }
}