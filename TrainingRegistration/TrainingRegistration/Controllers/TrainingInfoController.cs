using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingRegistration.Models;

namespace TrainingRegistration.Controllers
{
    public class TrainingInfoController : Controller
    {
        private TrainingContext db = new TrainingContext();

        // GET: TrainingInfo
        [Route("{pathInfo:string}")]
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
            ViewBag.Students = training.Students;
            return View("Index");
        }
    }
}