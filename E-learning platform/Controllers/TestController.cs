using E_learning_platform.Controllers;
using E_learning_platform.Extensions;
using E_learning_platform.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace E_learning_platform.Controllers
{
    [Authorize]
    public class TestController : BaseController
    {
        [Authorize(Roles = "Administrator,Moderator")]
        public async Task<ActionResult> Add(int? id)
        {
            if (id == null)
                return View("Error");
            var course = await db.Courses.FindAsync(id);
            if (course == null)
                return View("Error");
            return View(new TestAddViewModel() { CourseId = course.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Moderator")]
        public async Task<ActionResult> Add(TestAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var course = await db.Courses.FindAsync(model.CourseId);
                if (course == null)
                    return View("Error");
                var test = new Test()
                {
                    Name = model.Name,
                    QuestionLimit = model.QuestionLimit,
                    Course = course,
                    IsVisible = true
                };
                db.Tests.Add(test);
                foreach (var q in model.Questions)
                {
                    var question = new Question() { Text = q.Text };
                    foreach (var a in q.Answers)
                    {
                        var answer = new Answer() { Text = a.Text, IsCorrect = a.IsCorrect };
                        db.Answers.Add(answer);
                        answer.Question = question;
                    }
                    question.Test = test;
                    db.Questions.Add(question);
                }

                await db.SaveChangesAsync();
                return RedirectToAction("View", "Course", new { id = course.Id });
            }
            return View(model);
        }

        [Authorize(Roles = "Administrator,Moderator")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return View("Error");
            var test = await db.Tests.FindAsync(id);
            if (test == null)
                return View("Error");
            var questionList = new List<QuestionAddViewModel>();
            foreach (var q in test.Questions)
            {
                var question = new QuestionAddViewModel() { Id = q.Id, Text = q.Text };
                question.Answers = new List<AnswerAddViewModel>();
                foreach (var a in q.Answers)
                {
                    question.Answers.Add(new AnswerAddViewModel() { Text = a.Text, IsCorrect = a.IsCorrect, Id = a.Id });
                }
                questionList.Add(question);
            }
            return View(new TestAddViewModel()
            {
                Id = test.Id,
                Name = test.Name,
                CourseId = test.Course.Id,
                QuestionLimit = test.QuestionLimit,
                Questions = questionList
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Moderator")]
        public async Task<ActionResult> Edit(TestAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var test = await db.Tests.FindAsync(model.Id);
                if (test == null)
                    return View("Error");
                test.Name = model.Name;
                test.QuestionLimit = model.QuestionLimit;
                test.Questions.Clear();
                foreach (var q in model.Questions)
                {
                    var question = new Question() { Text = q.Text };
                    foreach (var a in q.Answers)
                    {
                        var answer = new Answer() { Text = a.Text, IsCorrect = a.IsCorrect };
                        db.Answers.Add(answer);
                        answer.Question = question;
                    }
                    question.Test = test;
                    db.Questions.Add(question);
                }
                await db.SaveChangesAsync();
                return RedirectToAction("View", "Course", new { id = model.CourseId });
            }
            return View(model);
        }

        [Authorize(Roles = "Administrator,Moderator")]
        public async Task<ActionResult> View(int? id)
        {
            if (id == null)
                return View("Error");
            var test = await db.Tests.FindAsync(id);
            if (test == null)
                return View("Error");
            return View(new TestDetailsViewModel()
            {
                Name = test.Name,
                Results = user.GeneratedTests.Where(t => t.Score != null && t.Test.Course.Id == test.Course.Id && t.Test.Id == test.Id).ToList(),
                CourseId = test.Course.Id,
                Id = test.Id
            });
        }

        [Authorize(Roles = "Administrator,Moderator")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return View("Error");
            var test = await db.Tests.FindAsync(id);
            if (test == null)
                return View("Error");
            return View(new TestDeleteViewModel()
            {
                Name = test.Name,
                Id = test.Id,
                CourseId = test.Course.Id
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Moderator")]
        public async Task<ActionResult> Delete(TestDeleteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var test = await db.Tests.FindAsync(model.Id);
                if (test == null)
                    return View("Error");
                test.IsVisible = false;
                await db.SaveChangesAsync();
                return RedirectToAction("View", "Course", new { id = model.CourseId });
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Solve(int? id)
        {
            if (id == null)
                return View("Error");
            var test = await db.Tests.FindAsync(id);
            if (test == null)
                return View("Error");
            var u = db.Users.Find(User.Identity.GetUserId());
            var generated = new GeneratedTest() { Date = DateTime.Now, Score = null, User = u, Test = test };
            db.GeneratedTests.Add(generated);
            var questions = test.Questions.ToList();
            questions.Shuffle(new Random());
            var testquestions = new List<Question>();
            for (int i = 0; i <= test.QuestionLimit - 1; i++)
            {
                if (i >= questions.Count)
                    break;
                var questionanswer = new UserQuestionAnswer() { Question = questions[i] };
                db.UserQuestionAnswers.Add(questionanswer);
                questionanswer.GeneratedTest = generated;
                testquestions.Add(questions[i]);
            }
            await db.SaveChangesAsync();
            return View(new TestSolveViewModel()
            {
                GeneratedId = generated.Id,
                Name = test.Name,
                CourseId = test.Course.Id,
                Questions = testquestions
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Check(TestCheckViewModel model)
        {
            if (ModelState.IsValid)
            {
                var generated = await db.GeneratedTests.FindAsync(model.GeneratedId);
                if (generated == null)
                    return View("Error");
                if (generated.Score != null || generated.User.Id != user.Id)
                    return View("Error");
                var userAnswers = model.UserAnswers;
                var score = 0;
                foreach (var element in generated.QuestionAnswers)
                {
                    var question = element.Question;
                    bool allAnswersCorrect = true;
                    foreach (var answer in question.Answers)
                    {
                        var answerId = answer.Id;
                        if(!userAnswers.ContainsKey(answerId))
                        {
                            allAnswersCorrect = false;
                            continue;
                        }
                        if (userAnswers[answerId] != answer.IsCorrect)
                            allAnswersCorrect = false;
                        if (userAnswers[answerId])
                            element.SelectedAnswers.Add(answer);

                    }
                    if (allAnswersCorrect)
                        score++;
                }
                generated.Score = score;
                await db.SaveChangesAsync();
                return RedirectToAction("Result", new { id = generated.Id });
            }
            return View("Error");
        }

        public async Task<ActionResult> Result(int? id)
        {
            if (id == null)
                return View("Error");
            var generated= await db.GeneratedTests.FindAsync(id);
            if (generated == null)
                return View("Error");
            return View(new TestResultViewModel()
            {
                Max = generated.QuestionAnswers.Count,
                Name = generated.Test.Name,
                CourseId = generated.Test.Course.Id,
                CourseName = generated.Test.Course.Name,
                Score = (int)generated.Score,
                UserQuestionAnswers = generated.QuestionAnswers.ToList()
            });
        }
    }
}