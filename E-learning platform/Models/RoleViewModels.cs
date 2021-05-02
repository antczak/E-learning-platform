using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_learning_platform.Models
{
    public class RoleIndexViewModel
    {
       public IEnumerable<IdentityRole> RoleList { get; set; }     
    }

    public class RoleDetailsViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Rola")]
        public string Name { get; set; }
        public IEnumerable<IdentityUser> UserList { get; set; }
    }

    public class RoleAssignViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Rola")]
        public string Name { get; set; }
        [Required]
        public string RoleId { get; set; }
    }
}