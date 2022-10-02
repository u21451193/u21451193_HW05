using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace u21451193_HW05.Models
{
    public class Borrows
    {
        public int BorrowsID { get; set; }
        public DateTime BorrowsTakenDate { get; set; }
        public DateTime BorrowsBroughtDate { get; set; }
        public string StudentName { get; set; }
        [Display(Name = "BookName")]
        public string BookName { get; set; }
    }
}