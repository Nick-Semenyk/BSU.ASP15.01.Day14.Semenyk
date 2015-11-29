using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TrainingRegistration.Models
{
    public class UniversitiesDbInitializer : DropCreateDatabaseAlways<TrainingContext>
    {
        protected override void Seed(TrainingContext db)
        {
         /*   db.Universities.Add(new University { Name = "БГУ", MaxYear = 5});
            db.Universities.Add(new University { Name = "БГУИР", MaxYear = 5 });
            db.Universities.Add(new University { Name = "БНТУ", MaxYear = 5 });
            db.Universities.Add(new University { Name = "БГЭУ", MaxYear = 4 });
            base.Seed(db);*/
        }
    }
}