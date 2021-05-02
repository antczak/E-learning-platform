using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace E_learning_platform.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public bool isAdmin { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public decimal Balance { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Course> ModeratedCourses { get; set; }
        public virtual ICollection<GeneratedTest> GeneratedTests { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseCategory> CourseCategories { get; set; }
        public DbSet<Presentation> Presentations { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<CFile> Files { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<GeneratedTest> GeneratedTests { get; set; }
        public DbSet<UserQuestionAnswer> UserQuestionAnswers { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}