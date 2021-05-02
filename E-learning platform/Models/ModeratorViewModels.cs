using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_learning_platform.Models
{
    public class ModeratorAddViewModel
    {
        [Required]
        [Display(Name = "Moderator")]
        public string ModeratorId { get; set; }
        public int CourseId { get; set; }
        public IEnumerable<ApplicationUser> ModeratorsList { get; set; }
    }

    public class ModeratorRemoveViewModel
    {
        [Required]
        public string ModId { get; set; }
        [Required]
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}