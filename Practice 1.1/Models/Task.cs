using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Practice_1._1.Models
{
    public class Task
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please type a title")]
        public string Title { get; set; }
        [MaxLength(30,ErrorMessage ="Description must be less than 30 letters")]
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public bool Completed { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }

    }
}