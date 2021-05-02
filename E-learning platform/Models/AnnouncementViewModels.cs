using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_learning_platform.Models
{
    public class AnnouncementAddViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        public string Text { get; set; }

        [Required]
        public int CourseId { get; set; }
    }
    public class AnnouncementDeleteViewModel
    {
        [Required]
        public int Id { get; set; }
        public string Title { get; set; }
        [Required]
        public int CourseId { get; set; }
    }
}