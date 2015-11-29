using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Protocols;
using TrainingRegistration.Models;

namespace TrainingRegistration.Controllers
{
    public class RegistrationController : Controller
    {
        private enum CheckState
        {
            FirstName,
            SecondName,
            Email,
            University,
            Year,
            Exists
        };

        private TrainingContext db = new TrainingContext();
        // GET: Registration
        public ActionResult Index()
        {
            return View("Error");
        }
        
        [Route("{trainingId:int}")]
        public ActionResult Execute(int trainingId)
        {
            IEnumerable<Training> t = db.Trainings.
                   Where(tr => tr.Id == trainingId);
            if (!t.Any())
            {
                return View("Error");
            }
            Training training = t.First();
            ViewBag.Training = training;
            IEnumerable<University> universities = db.Universities;
            ViewBag.Universities = universities;

            CheckState state = CheckState.FirstName;
            var post = Request.Form;
            //checking and resuming in case of error
            string firstName, secondName, email;
            int universityId = 1;
            int year;

            while (true)
            {
                if ((firstName = post["firstName"]) != null)
                {
                    Regex r = new Regex(@"[А-Я]{1,1}[А-Яа-я-]{0,}[а-я]{1,}");
                    Match m = r.Match(firstName);
                    if (m.Value.Length == 0)
                    {
                        break;
                    }
                }
                else break;
                state = CheckState.SecondName;
                if ((secondName = post["secondName"]) != null)
                {
                    Regex r = new Regex(@"[А-Я]{1,1}[А-Яа-я-]{0,}[а-я]{1,}");
                    Match m = r.Match(secondName);
                    if (m.Value.Length == 0)
                    {
                        break;
                    }
                }
                else break;
                state = CheckState.Email;
                if ((email = post["email"]) != null)
                {
                    Regex r = new Regex(@"([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})");
                    Match m = r.Match(email);
                    if (m.Value.Length == 0)
                    {
                        break;
                    }
                }
                else break;
                state = CheckState.University;
                if ((post["university"]) != null)
                {
                    if (!Int32.TryParse(post["university"], out universityId))
                        break;
                    if (universityId <= 0)
                        break;
                    IEnumerable<University> u = universities.
                        Where(university => university.Id == universityId);
                    if (!u.Any())
                    {
                        break;
                    }
                }
                else break;
                state = CheckState.Year;
                if ((post["year"]) != null)
                {
                    if (!Int32.TryParse(post["year"], out year))
                        break;
                    if (year <= 0)
                        break;
                    IEnumerable<University> u = universities.
                        Where(university => university.Id == universityId).
                        Where(university => university.MaxYear >= year);
                    if (!u.Any())
                    {
                        break;
                    }
                }
                else break;
                //everything is OK
                ViewBag.firstName = firstName;
                ViewBag.secondName = secondName;
                //check student existance
                IEnumerable<Student> s = db.Students.
                    Where(st =>
                        st.FirstName == firstName
                        && st.SecondName == secondName
                        && st.UniversityId == universityId
                        && st.Email == email);
                if (!s.Any())
                {
                    //create new student
                    db.Students.Add(new Student
                        {
                            FirstName = firstName,
                            SecondName = secondName,
                            Email = email,
                            UniversityId = universityId,
                            Grade = year,
                            Trainings = new List<Training>(t)
                        });
                    db.SaveChanges();
                }
                else
                {
                    //update existing
                    Student st = s.First();
                    //if already exists
                    if(st.Trainings.Contains(training))
                    {
                        state = CheckState.Exists;
                        break;
                    }
                    st.Trainings.Add(training);
                    db.Entry(st).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return View("Success");
            }
            //not first entry
            if (post["firstName"] != null)
                switch(state)
                {
                    case CheckState.FirstName:
                        ViewBag.Error = "Проверьте правильность ввода имени";
                        break;
                    case CheckState.SecondName:
                        ViewBag.Error = "Проверьте правильность ввода фамилии";
                        break;
                    case CheckState.Email:
                        ViewBag.Error = "Проверьте правильность ввода электронной почты";
                        break;
                    case CheckState.University:
                        ViewBag.Error = "Выберите университет из списка предложенных";
                        break;
                    case CheckState.Year:
                        ViewBag.Error = "Проверьте правильность ввода курса";
                        break;
                    case CheckState.Exists:
                        ViewBag.Error = "Этот студент уже зарегистрирован на этот тренинг";
                        break;
                }
            else
            {
                ViewBag.Error = "";
            }
            ViewBag.firstName = post["firstName"] ?? "";
            ViewBag.secondName = post["secondName"] ?? "";
            ViewBag.email = post["email"] ?? "";
            ViewBag.university = universityId;
            ViewBag.year = post["year"] ?? "1";
            return View("Index");
        }


    }
}