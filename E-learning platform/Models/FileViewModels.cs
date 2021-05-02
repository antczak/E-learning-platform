using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_learning_platform.Models
{
    public class FileAddViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Display(Name = "Plik")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase File { get; set; }

        [Required]
        public int CourseId { get; set; }
    }

    public class FileDeleteViewModel
    {
        [Required]
        public int Id { get; set; }
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Required]
        public int CourseId { get; set; }
    }
}