using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Practice_1._1.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public List<User> Users { get; set; }
    }
}