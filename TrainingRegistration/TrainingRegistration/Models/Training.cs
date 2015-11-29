using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainingRegistration.Models
{
    public class Training
    {
        public int Id { get; set; }
        public string Theme { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        
        public Training()
        {
            Students = new List<Student>();
        } 
    }
}