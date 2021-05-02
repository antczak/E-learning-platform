using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_learning_platform.Models
{
    public class PresentationAddViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Display(Name = "Plik PowerPoint")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase Presentation { get; set; }

        [Required]
        public int CourseId { get; set;}
    }

    public class PresentationDetailsViewModel
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public int CourseId { get; set; }
    }

    public class PresentationDeleteViewModel
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public int CourseId { get; set; }
    }
}