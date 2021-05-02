using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace E_learning_platform.Models
{
    public interface IOrderable
    {
        int Id { get; set; }
        int Order { get; set; }
    }

    public interface ICourseItem
    {
        int Id { get; set; }
        Course Course { get; set; }
    }
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string FullDesc { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public virtual CourseCategory Category { get; set; }
        public virtual ICollection<Presentation> Presentations { get; set; }
        public virtual ICollection<Test> Tests { get; set; }
        [InverseProperty("Courses")]
        public virtual ICollection<ApplicationUser> Users { get; set; }
        [InverseProperty("ModeratedCourses")]
        public virtual ICollection<ApplicationUser> Moderators { get; set; }
        public virtual ICollection<CFile> Files { get; set; }
        public virtual ICollection<Video> Videos { get; set; }
        public virtual ICollection<Announcement> Announcements { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public int Order { get; set; }
    }

    public class CourseCategory: IOrderable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string ImagePath { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public int Order { get; set; }
    }

    public class Presentation: IOrderable, ICourseItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int Order { get; set; }
        public virtual Course Course { get; set; }
    }

    public class Video: IOrderable, ICourseItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int Order { get; set; }
        public virtual Course Course { get; set; }
    }

    public class Test : IOrderable, ICourseItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public int QuestionLimit { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<GeneratedTest> GeneratedTests { get; set; }
        public virtual Course Course { get; set; }
        public bool IsVisible { get; set; }
    }

    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual Test Test { get; set; }
    }

    public class Answer
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public virtual Question Question { get; set; }
        public virtual ICollection<UserQuestionAnswer> QuestionAnswers { get; set; }
    }

    public class GeneratedTest
    {
        public int Id { get; set; }
        public virtual Test Test { get; set; }
        public int? Score { get; set; }
        public DateTime Date { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<UserQuestionAnswer> QuestionAnswers { get; set; }
    }

    public class UserQuestionAnswer
    {
        public int Id { get; set; }
        public virtual GeneratedTest GeneratedTest { get; set; }
        public virtual Question Question { get; set; }
        public virtual ICollection<Answer> SelectedAnswers { get; set; }
    }

    public class CFile : IOrderable, ICourseItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public string Path { get; set; }
        public virtual Course Course { get; set; }
    }

    public class Announcement
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public virtual Course Course { get; set; }
        public DateTime Date { get; set; }
    }

    public class Document : IOrderable, ICourseItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public virtual Course Course { get; set; }
        public DateTime Date { get; set; }
        public int Order { get; set; }
    }

}