using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_learning_platform.Models
{
    public class DocumentAddViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Required]
        [AllowHtml]
        public string Text { get; set; }

        [Required]
        public int CourseId { get; set; }
    }

    public class DocumentDetailsViewModel
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int CourseId { get; set; }
    }

    public class DocumentDeleteViewModel
    {
        [Required]
        public int Id { get; set; }
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Required]
        public int CourseId { get; set; }
    }
}