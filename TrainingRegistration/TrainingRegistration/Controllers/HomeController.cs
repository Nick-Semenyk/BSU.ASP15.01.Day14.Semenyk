using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingRegistration.Models;

namespace TrainingRegistration.Controllers
{
    public class HomeController : Controller
    {
        private TrainingContext db = new TrainingContext();
        
        public ActionResult Index()
        {
            IEnumerable<Training> trainings = db.Trainings;
            ViewBag.Trainings = trainings;
            return View();
        }
        
    }
}