using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_learning_platform.Models
{
    public class TestAddViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Ilość pytań do wylosowania")]
        public int QuestionLimit { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        public List<QuestionAddViewModel> Questions { get; set; }
    }

    public class TestDetailsViewModel
    {
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        public int CourseId { get; set; }
        public int Id { get; set; }
        public IEnumerable<GeneratedTest> Results { get; set; }
    }

    public class TestDeleteViewModel
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public int CourseId { get; set; }
    }
    public class QuestionAddViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public List<AnswerAddViewModel> Answers { get; set; }
    }

    public class AnswerAddViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public bool IsCorrect { get; set; }
    }

    public class TestSolveViewModel
    {
        public int GeneratedId { get; set; }
        public IEnumerable<Question> Questions { get; set; }
        public string Name { get; set; }
        public int CourseId { get; set; }

    }

    public class TestCheckViewModel
    {
        [Required]
        public int GeneratedId { get; set; }
        public IDictionary<int, bool> UserAnswers { get; set; }

    }

    public class TestResultViewModel
    {
        [Display(Name = "Wynik")]
        public int Score { get; set; }
        public int Max { get; set; }
        public IEnumerable<UserQuestionAnswer> UserQuestionAnswers { get; set; }
        [Display(Name = "Nazwa testu")]
        public string Name { get; set; }
        [Display(Name = "Nazwa kursu")]
        public string CourseName { get; set; }
        public int CourseId { get; set; }

    }
}