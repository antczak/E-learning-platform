using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_learning_platform.Models
{
    public class VideoAddViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Display(Name = "Plik wideo")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase Video { get; set; }

        [Required]
        public int CourseId { get; set; }
    }

    public class VideoDetailsViewModel
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public int CourseId { get; set; }
    }

    public class VideoDeleteViewModel
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public int CourseId { get; set; }
    }
}