using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_learning_platform.Models
{
    public class ProfileDetailsViewModel
    {
        [Display(Name = "Imię")]
        public string Name { get; set; }
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        public IList<Course> CourseList { get; set; }
        public IList<GeneratedTest> GeneratedTests { get; set; }
    }

    public class ProfileListViewModel
    {
        public IList<ApplicationUser> UserList { get; set; }
    }
}