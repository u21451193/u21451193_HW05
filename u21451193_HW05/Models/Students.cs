using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u21451193_HW05.Models
{
    public class Students
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string StudentSurname { get; set; }
        public DateTime StudentBirthDate { get; set; }
        public string StudentGender { get; set; }
        public string StudentClass { get; set; }
        public int StudentPoint { get; set; }
    }
}