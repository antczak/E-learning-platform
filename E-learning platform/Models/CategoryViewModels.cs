using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_learning_platform.Models
{
    public class CourseIndexViewModel
    {
        public IList<CourseCategory> CategoryList { get; set; }
    }

    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Course> CourseList { get; set; }
    }

    public class CategoryAddViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Opis")]
        public string Desc { get; set; }
    }

    public class CategoryDeleteViewModel
    {
        [Required]
        public int Id { get; set; }
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Display(Name = "Opis")]
        public string Desc { get; set; }
    }
}