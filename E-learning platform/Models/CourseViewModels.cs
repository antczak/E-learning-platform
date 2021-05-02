using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_learning_platform.Models
{

    public class CourseAddViewModel
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

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Cena")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Kategoria")]
        public int CategoryId { get; set; }

        [Display(Name = "Miniatura")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase Image { get; set; }

        public IEnumerable<CourseCategory> CourseCategoryList { get; set; }

    }

    public class CourseEditFullDescViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [AllowHtml]
        public string FullDesc { get; set; }
    }

    public class CourseDetailsViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Display(Name = "Cena")]
        public decimal Price { get; set; }
        public string FullDesc { get; set; }
        public bool hasAccess { get; set; }
        public IEnumerable<Presentation> Presentations { get; set; }
        public IEnumerable<CFile> Files { get; set; }
        public IEnumerable<Video> Videos { get; set; }
        public IEnumerable<Announcement> Announcements { get; set; }
        public IEnumerable<Test> Tests { get; set; }
        public IEnumerable<Document> Documents { get; set; }
        public IEnumerable<ApplicationUser> Moderators { get; set; }
    }

    public class CourseShortDetailsViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Display(Name = "Cena")]
        public decimal Price { get; set; }
        public bool hasAccess { get; set; }
        public bool canBuy { get; set; }
    }

    public class CourseDeleteViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Display(Name = "Cena")]
        public decimal Price { get; set; }
        [Display(Name = "Opis")]
        public string Desc { get; set; }
    }
}