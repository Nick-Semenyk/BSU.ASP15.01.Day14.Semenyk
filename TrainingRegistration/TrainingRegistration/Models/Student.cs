using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainingRegistration.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public int UniversityId { get; set; }
        public int Grade { get; set; }
        public virtual ICollection<Training> Trainings { get; set; }

        public Student()
        {
            Trainings = new List<Training>();
        }
    }
}